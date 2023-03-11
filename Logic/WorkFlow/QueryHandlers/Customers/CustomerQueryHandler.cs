using Data.Entities;
using Data.Shared;
using Logic.Core.UnitOfWork.IConfiguration;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.WorkFlow.QueryHandlers.Customers
{
    public class CustomerQueryHandler : QueryHandler
    {
        //CustomerQueryHandler tha borouse na einai se idia morfi me ton CustomerCommandHandler,
        // ean prokite px na exei polles morfes methodou Handle(), me diaforetikous tipous request
        //tote kalitera na ftiaxti fakellos opou tha exoume polles klaseis san Query
        public class Query : IRequest<ResponseList<Customer>>
        {
            public Paging Paging { get; set; }
            public Query(Paging paging)
            {
                Paging = paging;
            }
        }

        public class Handler : IRequestHandler<Query, ResponseList<Customer>>
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
