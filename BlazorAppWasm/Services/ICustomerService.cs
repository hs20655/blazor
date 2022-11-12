using DocumentFormat.OpenXml.Drawing.Charts;
using GridCore.Server;
using GridShared.Utility;
using GridShared;
using Microsoft.Extensions.Primitives;
using Data.Entities;
using GridBlazor.Pagination;
using Data.DTO.Responses;

namespace BlazorAppWasm.Services
{
    public interface ICustomerService
    {
        Task<ResponseList<Customer>> GetCustomers(int pageNumber, int pageSize);
    }
}
