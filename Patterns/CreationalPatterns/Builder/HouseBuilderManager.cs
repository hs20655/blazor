using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.CreationalPatterns.Builder
{
    public class HouseBuilderManager
    {
        public Type _builderType { get; set; }

        public void SetHouseBuilder(Type builderType)
        {
            _builderType = builderType;
        }
        public IHouseBuilder BuildMinimal(List<KeyValuePair<string, string>> parameters)
        {
            //WOOD HOUSE
            if(_builderType == typeof(WoodHouseBuilder))
            {
                var houseBuilder = new WoodHouseBuilder();
                foreach(var param in parameters)
                {
                    if (param.Key.ToLower() == "walls") houseBuilder.AddWalls(param.Value);
                    if (param.Key.ToLower() == "doors") houseBuilder.AddDoors(param.Value);
                    if (param.Key.ToLower() == "windows") houseBuilder.AddWindows(param.Value);
                    if (param.Key.ToLower() == "roof") houseBuilder.AddRoof(param.Value);
                }
                
                return houseBuilder;
            }
            else return null;
        }
        public IHouseBuilder BuildFullFeatured(List<KeyValuePair<string, string>> parameters)
        {
            //WOOD HOUSE
            if (_builderType == typeof(WoodHouseBuilder))
            {
                var houseBuilder = new WoodHouseBuilder();

                foreach (var param in parameters)
                {
                    if (param.Key.ToLower() == "walls") houseBuilder.AddWalls(param.Value);
                    if (param.Key.ToLower() == "doors") houseBuilder.AddDoors(param.Value);
                    if (param.Key.ToLower() == "windows") houseBuilder.AddWindows(param.Value);
                    if (param.Key.ToLower() == "roof") houseBuilder.AddRoof(param.Value);
                    if (param.Key.ToLower() == "sauna") houseBuilder.AddExtraWoodSauna(param.Value);
                }

                return houseBuilder;
            }
            else return null;
        }
    }
}
