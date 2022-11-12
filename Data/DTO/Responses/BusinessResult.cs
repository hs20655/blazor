using Data.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Responses
{
    public class BusinessResult<T>
    {
        public T? OperationResult { get; set; }
        public List<BrokenRule> BrokenRules { get; set; } = new List<BrokenRule>();

        public BusinessResult()
        {
            BrokenRules = new List<BrokenRule>();
        }
        public BusinessResult(T? result)
        {
            OperationResult = result;
            BrokenRules = new List<BrokenRule>();
        }
        public BusinessResult(T? result, List<BrokenRule> brokenRules)
        {
            OperationResult = result;
            BrokenRules = brokenRules;
        }
    }
}
