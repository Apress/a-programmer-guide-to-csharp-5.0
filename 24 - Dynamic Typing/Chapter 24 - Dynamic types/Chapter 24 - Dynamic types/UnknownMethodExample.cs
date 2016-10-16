using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_24___Dynamic_types
{
    public static class UnknownMethodExample
    {
        public static void Run()
        {
dynamic value = "a,b,c,d,e";

string[] items = value.SplitItUp(',');

foreach (string item in items)
{
    Console.WriteLine(item);
}
        }
    }
}
