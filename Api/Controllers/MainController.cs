using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        public IMediator _Mediator;

        public MainController(IMediator Mediator)
        {
            _Mediator = Mediator;
        }
    }
}
