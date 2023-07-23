using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.CreationalPatterns.Builder.HoseComponents
{
    public class StandartWallsComponent : IHouseComponent
    {
        public string ComponentName { get; set; }

        public string GetPartName()
        {
            return this.ComponentName;
        }
    }
}
