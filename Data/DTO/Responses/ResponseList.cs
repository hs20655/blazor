using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Responses
{
    public class ResponseList<T> : IResponse
    {
        public List<T> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
