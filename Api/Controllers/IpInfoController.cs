using Data.DTO.Requests.IpInfo;
using Data.Shared;
using Logic.WorkFlow.QueryHandlers.Customers;
using Logic.WorkFlow.QueryHandlers.IpInfo;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IpInfoController : MainController
    {
        [HttpPost("GetInfoByIp")]
        public async Task<IActionResult> GetInfoByIp([FromBody] IpInfoRequest request)
        {
           var response = await Mediator.Send(new IpInfoQueryHandler.Query(request));

           return Ok(response);
        }
    }
}
