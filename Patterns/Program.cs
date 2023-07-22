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

            CreationalPattern.Run();

            //BehavioralPattern.Run();

            //StructuralPattern.Run();

            Console.ReadKey();
        }

       
    }
}
