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
            Singleton();
            Prototype();
        }

        private static void Singleton()
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

        private static void Prototype()
        {
            
        }
    }
}
