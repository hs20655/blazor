using Data.DTO.Responses;
using Data.Entities;
using Logic.Core.UnitOfWork.IConfiguration;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Logic.WorkFlow.QuerieHandlers.Customers
{
    public class CustomersQueryHandler
    {
        public class Query : IRequest<ResponseList<Customer>>
        {
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public Query(int pageNumber, int pageSize)
            {
                PageNumber = pageNumber;
                PageSize = pageSize;
            }
        }

        private class Handler : IRequestHandler<Query, ResponseList<Customer>>
        {
            protected IUnitOfWork _unitOfWork;
            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ResponseList<Customer>> Handle(Query query, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Customers.PagedResult(query.PageNumber, query.PageSize);
            }
        }
    }
}
