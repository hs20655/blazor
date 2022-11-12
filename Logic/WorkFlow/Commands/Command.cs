using Data.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.WorkFlow.Commands
{
    public abstract class Command
    {
        public Guid Id { get; set; }
        public abstract bool IsValid(out List<BrokenRule> brokenRules);
    }
}
