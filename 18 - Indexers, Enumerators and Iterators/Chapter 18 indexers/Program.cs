using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_18_indexers
{
    class Program
    {
        static void Main(string[] args)
        {
IntListNew intList = new IntListNew(3);
intList.Add(1);
intList.Add(2);
intList.Add(4);

foreach (int number in intList.ReversedItems())
{
    Console.WriteLine(number);
}

MyListNew<int> myIntList = new MyListNew<int>(5);
myIntList.Add(1);
myIntList.Add(2);
myIntList.Add(4);
myIntList.Add(9);
myIntList.Add(16);

foreach (int number in myIntList.ReversedItems())
{
    Console.WriteLine(number);
}
        }
    }
}
