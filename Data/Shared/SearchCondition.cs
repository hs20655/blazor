using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Shared
{
    public class SearchCondition
    {
        public string Field { get; set; }
        public SearchOperation Operation { get; set; }
        public List<string> Values { get; set; }
        public bool IgnoreCase { get; set; } // Will implemented later
    }
}
