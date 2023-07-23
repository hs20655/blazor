using Patterns.CreationalPatterns.Builder.HoseComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Patterns.CreationalPatterns.Builder
{
    public class WoodHouseBuilder : IHouseBuilder
    {
        private WoodHouse _woodHouse;

        public WoodHouseBuilder()
        {
            _woodHouse = new WoodHouse();
        }

        public void AddDoors(string name)
        {
            _woodHouse.AddComponent(new SafetyDoorComponent() { ComponentName = name });
        }

        public void AddRoof(string name)
        {
            _woodHouse.AddComponent(new MetalicRoofComponent() { ComponentName = name });
        }

        public void AddWalls(string name)
        {
            _woodHouse.AddComponent(new StandartWallsComponent() { ComponentName = name });
        }

        public void AddWindows(string name)
        {
            _woodHouse.AddComponent(new PlasticWindowComponent() { ComponentName = name });
        }

        //SOME EXTRA TO ADD TO BASE HOUSE  COMPONENTS
        public void AddExtraWoodSauna(string name)
        {
            _woodHouse.AddComponent(new WoodSaunaComponent() { ComponentName = name });
        }

        public IHouse GetHouse()
        {
            return _woodHouse;
        }
    }
}
