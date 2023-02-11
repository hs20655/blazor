using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Shared
{
    public class Response<TPayload>
    {
        public TPayload Payload { get; set; }
        public List<BrokenRule> BrokenRules { get; set; } = new List<BrokenRule>();

    }
}
