using GridCore.Server;
using GridShared.Utility;
using GridShared;
using Microsoft.Extensions.Primitives;
using Data.Entities;
using BlazorAppWasm.Pages;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Net.Http;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web.Helpers;
using Data.DTO.Responses;
using Data.DTO.Requests;
using Azure.Core;
using GridBlazor.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization.Json;
using System.Net;
using BlazorAppWasm.Bing;
using static BlazorAppWasm.Bing.Resource;

namespace BlazorAppWasm.Services
{
    public class CustomerService : ICustomerService
    {
        //HTTP CLIENT FACTORY NEED TO USE
        //BaseAddress need to put to upsettings
        private readonly string baseAddress = @"https://localhost:7247/";
        private readonly string customerController = @"api/Customers/";
        private readonly string key = "Ave7u8IZmptJwvf7e6uoUwhywqpT8-R3ojqazHFUyuw6Gb6LZ_9sJc9-PUTffSqf";

        public async Task<ResponseList<Customer>> GetCustomers(int pageNumber, int pageSize)
        {            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //FetchData
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(customerController + "AllCustomers", new RequestList()
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    });
                   
                    return  await response.Content.ReadFromJsonAsync<ResponseList<Customer>>() ?? new ResponseList<Customer>();
                }
                catch (Exception e)
                {

                }

            }
            return new ResponseList<Customer>();
        }

        public async Task<BusinessResult<bool>> UpdateCustomer(Customer customer)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(customerController +"UpdateCustomer",
                        new UpdateCustomerRequest()
                        {
                            CustomerId = customer.Id.ToString(),
                            CompanyName = customer.CompanyName,
                            ContactName = customer.ContactName,
                            Address = customer.Address,
                            City = customer.City,
                            Region = customer.Region,
                            PostalCode = customer.PostalCode,
                            Country = customer.Country,
                            Phone = customer.Phone
                        });

                    var result = await response.Content.ReadFromJsonAsync<BusinessResult<bool>>();
                    return result ?? new BusinessResult<bool>() { OperationResult = false };
                }

                catch (Exception e)
                {

                }

            }

            return new BusinessResult<bool>() { OperationResult = false };
        }

        public async Task<BusinessResult<string>> AddCustomer(Customer customer)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(customerController + "AddCustomer",
                         new AddCustomerRequest()
                         {
                             CompanyName = customer.CompanyName,
                             ContactName = customer.ContactName,
                             Address = customer.Address,
                             City = customer.City,
                             Region = customer.Region,
                             PostalCode = customer.PostalCode,
                             Country = customer.Country,
                             Phone = customer.Phone
                         });

                    var result = await response.Content.ReadFromJsonAsync<BusinessResult<string>>();
                    return result ?? new BusinessResult<string>() { OperationResult = string.Empty };
                }
                catch (Exception e)
                {

                }

            }

            return new BusinessResult<string>() { OperationResult = string.Empty };
        }

        public async Task<BusinessResult<bool>> DeleteCustomer(Guid guid)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // will send object not just id in url string  so use post
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(customerController + "DeleteCustomer", 
                        new DeleteCustomerRequest() { CustomerId = guid.ToString() 
                        });

                    var result = await response.Content.ReadFromJsonAsync<BusinessResult<bool>>();
                    return result ?? new BusinessResult<bool>() { OperationResult = false };
                }
                catch (Exception e)
                {

                }

            }

            return new BusinessResult<bool>() { OperationResult = false }; ;
        }

        public async Task<Response> Autosuggest(string pointA, string pointB)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                        HttpResponseMessage response = await client.GetAsync(
                        $"https://dev.virtualearth.net/REST/V1/Routes/Walking?wp.0={pointA}&wp.1={pointB}&optmz=distance&output=json&key={key}"
                        ); 
                    return await response.Content.ReadFromJsonAsync<Response>() ?? new Response();
                }
                catch (Exception e)
                {

                }

            }
            return new Response();
        }

    }
}
