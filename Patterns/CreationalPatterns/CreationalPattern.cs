using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patterns.CreationalPatterns.Singleton;

namespace Patterns.CreationalPatterns
{
    public static class CreationalPattern
    {
        public static void Run()
        {
            Console.WriteLine("\n\nCreationlal Patterns\n");

            Singleton();

        }

        private static void  Singleton()
        {
            Console.WriteLine("***Singleton Example***");
            Console.WriteLine("Create 2 variables of SingletonExample class and compare them");
            
            SingletonExample singletonFirst = SingletonExample.GetInstance();
            SingletonExample singletonSecond = SingletonExample.GetInstance();


            if (singletonFirst == singletonSecond)
            {
                Console.WriteLine("Singleton works, both variables contain the same instance.");
                singletonFirst.BusinessLogic();
            }
               
            else
                Console.WriteLine("Singleton failed, variables contain different instances.");

            Console.WriteLine("Press any key to see next example");
        }
    }
}
