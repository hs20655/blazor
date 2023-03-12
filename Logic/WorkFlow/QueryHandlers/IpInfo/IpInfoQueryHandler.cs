using Abp.Runtime.Caching;
using Castle.Core.Internal;
using Castle.MicroKernel.Util;
using Data.DTO.Requests.IpInfo;
using Data.DTO.Responses.IpInfo;
using Data.Entities;
using Data.Shared;
using Logic.Core.UnitOfWork.IConfiguration;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.WorkFlow.QueryHandlers.IpInfo
{
    public sealed class IpInfoQueryHandler : QueryHandler
    {
        //IpInfoQueryHandler tha borouse na einai se idia morfi me ton CommandHandler xoris nested class,
        // ean prokite px na exei polles morfes methodou Handle(), me diaforetikous tipous request
        // tote kalitera na ftiaxti fakellos opou tha exoume polles klaseis san Query
        public class Query : IRequest<Response<IpInfoResponse>>
        {
            public IpInfoRequest Request { get; set; }
            public Query(IpInfoRequest request)
            {
                Request = request;
            }
        }


        public class Handler :  IRequestHandler<Query, Response<IpInfoResponse>>
        {
            private IUnitOfWork _unitOfWork;
            private IMemoryCache _memoryCache;
            private IHttpClientFactory _clientFactory;
            

            public Handler(IUnitOfWork unitOfWork, IMemoryCache memoryCache, IHttpClientFactory clientFactory)
            {
                _unitOfWork = unitOfWork;
                _memoryCache= memoryCache;
                _clientFactory= clientFactory;
            }

            public async Task<Response<IpInfoResponse>> Handle(Query query, CancellationToken cancellationToken)
            {
                var response = new Response<IpInfoResponse>();

                // validate ip address for null....

                //Check in MemoryChashe
                _memoryCache.TryGetValue(query.Request.IpAddress, out IpInfoResponse? ipInfoFromChache);
                
                if(ipInfoFromChache == null)
                {
                    //Check in DataBase
                    var ipInfoFromDb = await _unitOfWork.IpAddresses.GetCountryDataByIp(query.Request.IpAddress);
                    if(ipInfoFromDb == null)
                    {
                        //make httpFactory call to ex https://ip2c.org/62.74.227.62  
                        HttpResponseMessage httpResponse;
                        string responseFromNetwork = string.Empty;
                        using var client = _clientFactory.CreateClient();
                        
                        try
                        {
                            httpResponse = await client.GetAsync($"https://ip2c.org/{query.Request.IpAddress}");// NEED TO ADD https://ip2c.org/ TO appsettings.json

                            if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK) 
                                return response;// write to log 

                            responseFromNetwork = await httpResponse.Content.ReadAsStringAsync();
                        }
                        catch (Exception)
                        {
                            //write to log 
                        }

                        if (!responseFromNetwork.IsNullOrEmpty())
                        {
                            var responseToArray = responseFromNetwork.Split(';', StringSplitOptions.RemoveEmptyEntries);
                            if(responseToArray.Length >= (int)IpInfoEnnum.TotalFields)
                            {
                                response.Result = new IpInfoResponse();
                                response.Result.CountryName = responseToArray[(int)IpInfoEnnum.CountryName];
                                response.Result.TwoLetterCode = responseToArray[(int)IpInfoEnnum.TwoLetterCode];
                                response.Result.ThreeLetterCode = responseToArray[(int)IpInfoEnnum.ThreeLetterCode];
                                
                                // store in MemoryCache
                                _memoryCache.Set(query.Request.IpAddress, response.Result);
                                //store in Database
                                //check if country exist in Countries table
                                var country = await _unitOfWork.Countries.GetFirstOrDefault(i => i.Name == response.Result.CountryName);


                                if (country != null)
                                {
                                    country.IpAddresses.Add(new IpAddress()
                                    {
                                        Ip = query.Request.IpAddress,
                                        CreatedDate = DateTime.UtcNow,
                                        CreatedByUserId = 1
                                    });
                                }
                                else
                                {
                                    //if not exist, add new country to Country table and new ip to IpAddresses table
                                    await _unitOfWork.Countries.Add(
                                        new Country()
                                        {
                                            Name = response.Result.CountryName,
                                            TwoLetterCode = response.Result.TwoLetterCode,
                                            ThreeLetterCode = response.Result.ThreeLetterCode,
                                            CreatedByUserId = 1,// it is for test, need guid
                                            CreatedDate = DateTime.UtcNow,
                                            IpAddresses = new List<IpAddress>()
                                            {
                                                new IpAddress()
                                                {
                                                   Ip = query.Request.IpAddress,
                                                   CreatedDate= DateTime.UtcNow,
                                                   CreatedByUserId = 1
                                                }
                                            }
                                        },
                                        cancellationToken);
                                }
                                //save changes
                                await _unitOfWork.CompleteAsync(cancellationToken);
                            }
                        }
                    }
                    else
                    {
                        // return result from db
                        response.Result = ipInfoFromDb;
                        // add to MemoryChash
                        _memoryCache.Set(query.Request.IpAddress, ipInfoFromDb);
                    } 
                }
                else 
                    response.Result = ipInfoFromChache; //return from cash

                return response;
            }
           
        }
    }
}
