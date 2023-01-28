using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Data;

namespace Api.Controllers
{
    [Authorize(Roles = "admin")]
    public class CustomersOdataController : ControllerBase
    {
        private ModelContext _modelContext;
        public CustomersOdataController(ModelContext modelContext)
        {
            _modelContext = modelContext;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            return Ok(_modelContext.Customers.AsQueryable());
        }
    }
}
