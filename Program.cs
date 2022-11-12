using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace Test003
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch sw = new Stopwatch();
            //MY
            string widthField = "width";
            string heightField = "height";
            string positionField = "position";
            string rotationField = "rotation";
            //

            // ORIGINAL BEGIN
            sw.Start();
            List<ReflectionClass<MyClass>> reflectors = new List<ReflectionClass<MyClass>>();
            for (int i = 0; i < 1000; i++)
            {
                MyClass myClass = new MyClass();
                ReflectionClass<MyClass> reflectionClass = new ReflectionClass<MyClass>(myClass);
                reflectors.Add(reflectionClass);
            }
            sw.Stop();
            Console.Write("ORIGINAL CODE (PART 1) in miliseconds: " + sw.Elapsed.TotalMilliseconds +"\n");

            sw.Reset();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                for (int k = 0; k < reflectors.Count; k++)
                {
                    int width = (int)reflectors[k].GetValue("width");
                    reflectors[k].SetValue("width", width + new Random().Next());

                    int height = (int)reflectors[k].GetValue("height");
                    reflectors[k].SetValue("height", height + new Random().Next());

                    Vector3 position = (Vector3)reflectors[k].GetValue("position");
                    reflectors[k].SetValue("position", position + new Vector3(10, 0, 0));

                    Quaternion quaternion = (Quaternion)reflectors[k].GetValue("rotation");
                    reflectors[k].SetValue("rotation", quaternion + new Quaternion(10, 0, 10, 0));
                }
            }
            sw.Stop();
            Console.Write("ORIGINAL CODE (PART 2) in miliseconds: " + sw.Elapsed.TotalMilliseconds + "\n");
            // ORIGINAL EMD

            ////MY BEGIN
            sw.Reset();
            sw.Start();
            List<ReflectionClass<MyClass>> reflectorsTARAS = new List<ReflectionClass<MyClass>>();
            for (int i = 0; i < 1000; i++)
            {
                reflectorsTARAS.Add(new ReflectionClass<MyClass>(new MyClass()));
            }
            sw.Stop();
            Console.Write("MY CODE (PART 1) in miliseconds: " + sw.Elapsed.TotalMilliseconds + "\n");

            sw.Reset();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                for (int k = 0; k < reflectorsTARAS.Count; k++)
                {
                    reflectorsTARAS[k].SetValue(widthField, (int)reflectorsTARAS[k].GetValue(widthField) + new Random().Next());

                    reflectorsTARAS[k].SetValue(heightField, (int)reflectorsTARAS[k].GetValue(heightField) + new Random().Next());

                    reflectorsTARAS[k].SetValue(positionField, (Vector3)reflectorsTARAS[k].GetValue(positionField) + new Vector3(10, 0, 0));

                    reflectorsTARAS[k].SetValue(rotationField, (Quaternion)reflectorsTARAS[k].GetValue(rotationField) + new Quaternion(10, 0, 10, 0));
                }
            }
            sw.Stop();
            Console.Write("MY CODE (PART 2) in miliseconds: " + sw.Elapsed.TotalMilliseconds + "\n");
            /////MY END





            Console.ReadKey();
        }
    }
}
