using Patterns.CreationalPatterns.Builder.HoseComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.CreationalPatterns.Builder
{
    public class WoodHouse : IHouse
    {
        private List<IHouseComponent> _components;
        public WoodHouse()
        {
            _components = new List<IHouseComponent>();
        }

        public void AddComponent(IHouseComponent component) 
        {
            this._components.Add(component);
        }
    }
}
