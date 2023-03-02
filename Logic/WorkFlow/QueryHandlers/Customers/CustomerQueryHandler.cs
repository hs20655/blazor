using Data.Entities;
using Data.Shared;
using Logic.WorkFlow.CommandHandlers;
using Logic.Core.UnitOfWork.IConfiguration;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.WorkFlow.QueryHandlers.Customers
{
    public class CustomerQueryHandler
    {
        public class Query : IRequest<ResponseList<Customer>>
        {
            public Paging Paging { get; set; }
            public Query(Paging paging)
            {
                Paging = paging;
            }
        }

        public class Handler : CommandHandler, IRequestHandler<Query, ResponseList<Customer>>
        {
            protected IUnitOfWork _unitOfWork;
            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ResponseList<Customer>> Handle(Query query, CancellationToken cancellationToken)
            {
                var paging = await _unitOfWork.Customers.Paging(query.Paging);
                var result = new ResponseList<Customer>()
                {
                    Timestamp = paging.Timestamp,
                    Payload = paging.Result,
                    TotalRecords = paging.Total
                };
                return result;
            }


        }
    }
}
