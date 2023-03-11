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
    public class IpInfoQueryHandler : QueryHandler
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
            protected IUnitOfWork _unitOfWork;
            protected IMemoryCache _memoryCache;
            protected IHttpClientFactory _clientFactory;
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

                //Check in MemoryChash
                _memoryCache.TryGetValue(query.Request.IpAddress, out IpInfoResponse? ipInfoFromChach);
                
                if(ipInfoFromChach == null)
                {
                    //Check in DataBase
                    var ipInfoFromDb = await _unitOfWork.IpAddresses.GetCountryDataByIp(query.Request.IpAddress);
                    if(ipInfoFromDb == null)
                    {
                        //make httpFactory call to ex https://ip2c.org/62.74.227.62
                        var client = _clientFactory.CreateClient();
                        var httpResponse = await client.GetAsync($"https://ip2c.org/{query.Request.IpAddress}");// NEED TO ADD https://ip2c.org/ TO appsettings.json
                        string responseFromNetwork = await httpResponse.Content.ReadAsStringAsync();
                        
                        if(!responseFromNetwork.IsNullOrEmpty())
                        {
                            var responseToArray = responseFromNetwork.Split(';', StringSplitOptions.RemoveEmptyEntries);
                            if(responseToArray.Length == 4)
                            {
                                response.Result = new IpInfoResponse();
                                response.Result.CountryName = responseToArray[3];
                                response.Result.TwoLetterCode = responseToArray[1];
                                response.Result.ThreeLetterCode = responseToArray[2];
                                // store in MemoryCash
                                _memoryCache.Set(query.Request.IpAddress, response.Result);
                                
                                //store in Database
                                //check if country exist in Countries table
                                var country = await _unitOfWork.Countries.GetFirstOrDefault(i => i.Name == response.Result.CountryName);

                                if (country != null)
                                {
                                    // if exist, add only new 
                                    await _unitOfWork.IpAddresses.Add(
                                    new IpAddress()
                                    {
                                        CountryId= country.Id,
                                        Ip = query.Request.IpAddress,
                                        CreatedDate = DateTime.UtcNow,
                                        CreatedByUserId = 1
                                    }, 
                                    cancellationToken);
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
                    response.Result = ipInfoFromChach; //return from cash



                return response;
            }
           
        }
    }
}
