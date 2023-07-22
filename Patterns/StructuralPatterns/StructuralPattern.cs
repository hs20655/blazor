using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.StructuralPatterns
{
    public static class StructuralPattern
    {
        public static void Run()
        {
            //Adapter is a structural design pattern that allows objects with incompatible interfaces to collaborate.
            AdapterExecute();
            
            //Bridge is a structural design pattern that lets you split a large class or a set of closely related classes
            //into two separate hierarchies—abstraction and implementation—which can be developed independently of each other.
            BridgeExecute();
            
            //Composite is a structural design pattern that lets you compose objects into tree structures and then
            //work with these structures as if they were individual objects.
            CompositeExecute();
            
            //Decorator is a structural design pattern that lets you attach new behaviors to objects by placing these objects
            //inside special wrapper objects that contain the behaviors.
            DecoratorExecute();
           
            //Facade is a structural design pattern that provides a simplified interface to a library, a framework, or any other complex set of classes.
            FacadeExecute();
            
            //Flyweight is a structural design pattern that lets you fit more objects into the available amount of RAM
            //by sharing common parts of state between multiple objects instead of keeping all of the data in each object
            FlyweightExecute();
            
            //Proxy is a structural design pattern that lets you provide a substitute or placeholder for another object.
            //A proxy controls access to the original object, allowing you to perform something either before
            //or after the request gets through to the original object.
            ProxyExecute();
        }

        private static void AdapterExecute()
        {

        }
        private static void BridgeExecute()
        {

        }
        private static void CompositeExecute()
        {

        }
        private static void DecoratorExecute()
        {

        }
        private static void FacadeExecute()
        {

        }
        private static void FlyweightExecute()
        {

        }
        private static void ProxyExecute()
        {

        }
    }
}
