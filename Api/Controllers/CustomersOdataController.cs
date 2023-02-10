using Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Data;

namespace Api.Controllers
{
    [Authorize(Roles = "admin")]
    public class CustomersOdataController : ODataController
    {
        private ModelContext _modelContext;
        public CustomersOdataController(ModelContext modelContext)
        {
            _modelContext = modelContext;
        }

        [EnableQuery]
        public async Task<IActionResult> Get(ODataQueryOptions<Customer> options)
        {
            var filter = options.Filter;
            var result = _modelContext.Customers.AsQueryable();
            

            // var queryResults = (IQueryable<Customer>)queryOptions.ApplyTo(query);
            //return Ok(new PageResult<Customer>(queryResults, Request.ODataProperties().NextLink, Request.ODataProperties().TotalCount));
            //return Ok(new PageResult<Customer>(queryResults);

            // return Ok(result);
            return Ok(result);
        }
    }
}
