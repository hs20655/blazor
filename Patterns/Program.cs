using Patterns.BehavioralPatterns;
using Patterns.CreationalPatterns;
using Patterns.StructuralPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pattens in C#");
            //Creational design patterns provide various object creation mechanisms,
            //which increase flexibility and reuse of existing code.
            CreationalPattern.Run();

            //Behavioral design patterns are concerned with algorithms and
            //the assignment of responsibilities between objects.
            BehavioralPattern.Run();

            //Structural design patterns explain how to assemble objects
            //and classes into larger structures, while keeping these structures flexible and efficient.
            StructuralPattern.Run();

            Console.ReadKey();
        }

       
    }
}
