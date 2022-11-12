using Logic.Core.WorkFlow.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Requests
{
    public class RequestList : IRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        
        //T Filters
    }
}
