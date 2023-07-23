using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.CreationalPatterns.Builder.HoseComponents
{
    public interface IHouseComponent
    {
        string ComponentName { get; set; }
        string GetPartName();
    }
}
