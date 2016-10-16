using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_24___Dynamic_types
{
    public static class SimpleExample
    {
        public static void Run()
        {
            Employee george = new Employee("George", 15, 10M);
            var jane = new Employee("Jane", 13, 9M);
            dynamic astro = new Employee("Rastro", 7, 1M);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            PropertyInfo nameInfo = typeof(Employee).GetProperty("Name");
            string name = (string)nameInfo.GetValue(george);
            Console.WriteLine(name);

            for (int i = 0; i < 1000 * 10000; i++)
            {
                //string v = george.Name;
                //string v = astro.Name;
                string v = (string)nameInfo.GetValue(george);

                int result = george.GetValue();
                //int result = jane.GetValue();
                //int result = astro.GetValue();
            }

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            Console.WriteLine(george.Name);
            Console.WriteLine(jane.Name);
            string t = astro.Name;

            //            Console.WriteLine(astro.ICanWriteAnythingHere);
        }
    }
}
