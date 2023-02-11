using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Shared
{
    public class ResponseList<T>
    {
        public List<T> Payload { get; set; }
        public int? TotalRecords { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
