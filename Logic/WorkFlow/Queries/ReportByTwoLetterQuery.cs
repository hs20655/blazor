using Data.DbQueryObjects;
using Data.DTO.Responses.IpInfo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.WorkFlow.Queries
{
    public class ReportByTwoLetterQuery : IRequest<List<TwoLetterReportDbQueryResponse>>
    {
        public string TwoLetter { get; set; }
        public ReportByTwoLetterQuery(string twoLetter)
        {
            TwoLetter = twoLetter;
        }

        
    }
}
