using Azure.Core;
using Data.DTO.Requests;
using Data.Entities;
using Logic.WorkFlow.Commands.Customer;
using Logic.WorkFlow.QuerieHandlers.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private IMediator _Mediator;
        public CustomersController(IMediator Mediator)
        {
            _Mediator = Mediator;
        }

        [HttpPost("AllCustomers")]
        public async Task<IActionResult> AllCustomers([FromBody] RequestList request)
        {
            var response = await this._Mediator.Send(new CustomersQueryHandler.Query(request.PageNumber, request.PageSize));
            return Ok(response);
        }


        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerRequest request)
        {
            var response = await this._Mediator.Send(new AddCustomerCommand() 
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
            var response = await this._Mediator.Send(new UpdateCustomerCommand()
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

         // will send object not just id in url string  so use post
        [HttpPost("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer([FromBody] DeleteCustomerRequest request)
        {
            var response = await this._Mediator.Send(new DeleteCustomerCommand()
            {
                Id = Guid.Parse(request.CustomerId),
            });
            return Ok(response);
        }
    }
}
