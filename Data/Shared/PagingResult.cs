using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Shared
{
    public class PagingResult<T>
    {
        public DateTime? Timestamp { get; set; }
        public List<T> Result { get; set; }
        public int? Total { get; set; }
    }
}
