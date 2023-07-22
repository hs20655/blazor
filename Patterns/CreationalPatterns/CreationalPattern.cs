using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }
    }
}
