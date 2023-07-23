using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.CreationalPatterns.Builder
{
    public interface IHouseBuilder
    {
        //All hose has walls, roof, doors, windows 
        void AddWalls(string name);
        void AddRoof(string name);
        void AddDoors(string name);
        void AddWindows(string name);

        IHouse GetHouse();
    }
}
