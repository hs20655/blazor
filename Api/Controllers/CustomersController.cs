using Azure.Core;
using Data;
using Data.DTO.Requests.Customer;
using Data.Entities;
using Data.Shared;
using Logic.WorkFlow.Commands.Customer;
using Logic.WorkFlow.QueryHandlers.Customers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Authorize(Roles = "admin")] //[Authorize(Roles = "admin, manager...")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : MainController
    {
        [HttpPost("GetCustomers")]
        public async Task<IActionResult> GetClients(Paging request)
        {
            var response = await Mediator.Send(new CustomerQueryHandler.Query(request));
            return Ok(response);
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerRequest request)
        {
            var response = await Mediator.Send(new AddCustomerCommand()
            {
                CompanyName = request.CompanyName,
                ContactName = request.ContactName,
                Address = request.Address,
                City = request.City,
                Region = request.Region,
                PostalCode = request.PostalCode,
                Country = request.Country,
                Phone = request.Phone
            });
            return Ok(response);
        }

        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerRequest request)
        {
            var response = await Mediator.Send(new UpdateCustomerCommand()
            {
                Id = Guid.Parse(request.CustomerId),
                CompanyName = request.CompanyName,
                ContactName = request.ContactName,
                Address = request.Address,
                City = request.City,
                Region = request.Region,
                PostalCode = request.PostalCode,
                Country = request.Country,
                Phone = request.Phone
            });
            return Ok(response);
        }

        [HttpPost("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer([FromBody] DeleteCustomerRequest request)
        {
            var response = await Mediator.Send(new DeleteCustomerCommand()
            {
                Id = Guid.Parse(request.CustomerId),
            });
            return Ok(response);
        }
    }
}
