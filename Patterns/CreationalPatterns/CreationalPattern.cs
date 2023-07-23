using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patterns.CreationalPatterns.Builder;
using Patterns.CreationalPatterns.Prototype;
using Patterns.CreationalPatterns.Singleton;

namespace Patterns.CreationalPatterns
{
    public static class CreationalPattern
    {
        public static void Run()
        {
            //Singleton is a creational design pattern that lets you ensure that a class has only one instance,
            //while providing a global access point to this instance.
            SingletonExecute();
            
            //Prototype is a creational design pattern that lets you copy existing objects
            //without making your code dependent on their classes.
            PrototypeExecute();

            //Builder is a creational design pattern that lets you construct complex objects step by step.
            //The pattern allows you to produce different types and representations of an object using the same construction code.
            BuilderExecute();

            //Abstract Factory is a creational design pattern that lets you produce families of related objects without specifying their concrete classes.
            AbstractFactoryExecute();

            //Factory Method is a creational design pattern that provides an interface for creating objects in a superclass,
            //but allows subclasses to alter the type of objects that will be created.
            FactoryExecute();
        }

        private static void SingletonExecute()
        {
            SingletonExample singletonFirst = SingletonExample.GetInstance();
            SingletonExample singletonSecond = SingletonExample.GetInstance();


            if (singletonFirst == singletonSecond)
            {
                //Singleton Works
                singletonFirst.BusinessLogic();
            }
               
            else
            {
                //ERROR
            }
        }
        private static void PrototypeExecute()
        {
            //initiate prototype person1 .Create person2 and person3 same like person1
            PrototypeExample prototypeExample = new PrototypeExample(34, Convert.ToDateTime("1989-04-01"), "Taras", 12345);
            //get colned objects   
            var clones = prototypeExample.GetPrototypeAndClones();
            //modify orignal prototype (person1)
            prototypeExample.ModifyPrototype(null, null, null, null);
            //get cloned object after modifying original prototype (check results)
            var clonesAfter = prototypeExample.GetPrototypeAndClones();
        }
        private static void BuilderExecute()
        {
            //create house buiild manager
            var houseBuilderManager = new HouseBuilderManager();
            //set type of house to build
            houseBuilderManager.SetHouseBuilder(typeof(WoodHouseBuilder));

            var minimalWoodHousehouse = houseBuilderManager.BuildMinimal(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("walls","ITALIAN walls"),
                new KeyValuePair<string, string>("doors","ITALIAN doors"),
                new KeyValuePair<string, string>("windows","ITALIAN windows"),
                new KeyValuePair<string, string>("roof","ITALIAN roof")
            }).GetHouse();
            
            var fullFeaturedWoodHousehouse = houseBuilderManager.BuildFullFeatured(new List<KeyValuePair<string, string>> () 
            {
                new KeyValuePair<string, string>("walls","GERMAN walls"),
                new KeyValuePair<string, string>("doors","GERMAN doors"),
                new KeyValuePair<string, string>("windows","GERMAN windows"),
                new KeyValuePair<string, string>("roof","GERMAN roof"),
                new KeyValuePair<string, string>("sauna","GERMAN sauna")
            }).GetHouse();

            var fullFeaturedWoodHousehouse2 = houseBuilderManager.BuildFullFeatured(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("walls","SPANISH walls"),
                new KeyValuePair<string, string>("doors","SPANISH doors"),
                new KeyValuePair<string, string>("windows","SPANISH windows"),
                new KeyValuePair<string, string>("roof","SPANISH roof"),
                new KeyValuePair<string, string>("sauna","SPANISH sauna")
            }).GetHouse();

            //SET here other tyoe of house buider px SpesialHouseBuilder..

            var houses = new List<IHouse>() { minimalWoodHousehouse, fullFeaturedWoodHousehouse, fullFeaturedWoodHousehouse2 };
        }
        private static void AbstractFactoryExecute()
        {

        }
        private static void FactoryExecute()
        {

        }
    }
}
