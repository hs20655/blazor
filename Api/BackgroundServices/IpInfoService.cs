using Azure;
using Data;
using Data.DTO;
using Data.DTO.Responses.IpInfo;
using Data.Entities;
using Data.Shared;
using Logic.Core.UnitOfWork.IConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Dynamic.Core;

namespace Api.BackgroundServices
{
    public sealed class IpInfoService : BackgroundService
    {
        private const int DELAY = 180000; // 3 minute, need to set one hour //need to add to appsettings json
        private const int BATCH_TOTAL_RECORDS = 2;//100; need to add to appsettings json
        private IMemoryCache _memoryCache;
        private IHttpClientFactory _clientFactory;
        

        public IpInfoService(IMemoryCache memoryCache, IHttpClientFactory clientFactory)
        {
            _memoryCache = memoryCache;
            _clientFactory = clientFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
         
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(DELAY, cancellationToken);
                await this.SyncDatabase();
            }
        }

        private async Task<Task<string>> SyncDatabase()
        {
            using(ModelContext context = new ModelContext())
            {
                using var client = _clientFactory.CreateClient();
                HttpResponseMessage httpResponse;
                string responseFromNetwork = string.Empty;
                var lastIpInformation = new IpInfoResponse();
                var ipAddressDbSet = context.Set<IpAddress>();
                var countriesDbSet = context.Set<Country>();
                
                for (int i = 0; i < ipAddressDbSet.Count(); i+= BATCH_TOTAL_RECORDS)
                {
                    var batch = ipAddressDbSet.Include(i => i.Country).Skip(i).Take(BATCH_TOTAL_RECORDS);
                    foreach (var storedIpInformation in batch)
                    {
                        try
                        {
                            httpResponse = await client.GetAsync($"https://ip2c.org/{storedIpInformation.Ip}"); // need to add to appsettings json
                            responseFromNetwork = await httpResponse.Content.ReadAsStringAsync();
                            if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
                                continue;// write to log, 

                            var responseToArray = responseFromNetwork.Split(';', StringSplitOptions.RemoveEmptyEntries);
                            
                            if(responseToArray.Length >= (int)IpInfoEnnum.TotalFields)
                            {

                                lastIpInformation.CountryName = responseToArray[(int)IpInfoEnnum.CountryName];
                                lastIpInformation.TwoLetterCode = responseToArray[(int)IpInfoEnnum.TwoLetterCode];
                                lastIpInformation.ThreeLetterCode = responseToArray[(int)IpInfoEnnum.ThreeLetterCode];

                                bool isSame = CompareIpInfo(storedIpInformation, lastIpInformation);
                                
                                if (!isSame)
                                {
                                    // example: if selected ip address was removed from ip adresses of current country

                                    //REMOVE from database
                                    ipAddressDbSet.Remove(storedIpInformation);
                                    //REMOVE from chache
                                    if(_memoryCache.TryGetValue(storedIpInformation.Ip, out IpInfoResponse? ipAddressInChache))
                                    {
                                        _memoryCache.Remove(storedIpInformation.Ip);
                                    }
                                    //ADD to database and 
                                    //VALIDATE IF county already exists in database
                                    var countryExist = countriesDbSet.Where(i => i.Name.Equals(lastIpInformation.CountryName))
                                        .Include(i => i.IpAddresses).FirstOrDefault();
                                    
                                    if(countryExist != null)
                                    {
                                        countryExist.IpAddresses.Add(new IpAddress() 
                                        {
                                            Ip = storedIpInformation.Ip,
                                            CreatedDate = DateTime.UtcNow,
                                            CreatedByUserId = 1 // for test
                                        });
                                    }
                                    else
                                    {
                                        countriesDbSet.Add(new Country()
                                        {
                                            Name = lastIpInformation.CountryName,
                                            TwoLetterCode = lastIpInformation.TwoLetterCode,
                                            ThreeLetterCode = lastIpInformation.ThreeLetterCode,
                                            CreatedByUserId = 1,// it is for test, need guid
                                            CreatedDate = DateTime.UtcNow,
                                            IpAddresses = new List<IpAddress>()
                                            {
                                                new IpAddress()
                                                {
                                                   Ip = storedIpInformation.Ip,
                                                   CreatedDate= DateTime.UtcNow,
                                                   CreatedByUserId = 1
                                                }
                                            }
                                        });
                                    }

                                    await context.SaveChangesAsync();
                                }
                                else
                                {
                                    string ok = string.Empty;// for test
                                }
                            }
                           
                        }
                        catch (Exception)
                        {
                           // write to log
                        }
                        
                    }
                }

            }

            return Task.FromResult("Completed");
        }


        private static bool CompareIpInfo(IpAddress storedIpInformation, IpInfoResponse lastIpInformation)
        {
            if(!lastIpInformation.CountryName.Equals(storedIpInformation.Country.Name))
                return false;
            if (!lastIpInformation.TwoLetterCode.Equals(storedIpInformation.Country.TwoLetterCode))
                return false;
            if (!lastIpInformation.ThreeLetterCode.Equals(storedIpInformation.Country.ThreeLetterCode))
                return false;

            return true;
        }

    }
}
