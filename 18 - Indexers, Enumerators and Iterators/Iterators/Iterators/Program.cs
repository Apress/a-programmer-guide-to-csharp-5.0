#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Iterators {
 class Program {
  static void Main(string[] args)
  {
   IntList il = new IntList();
   il.Add(1);
   il.Add(2);
   il.Add(3);
   il.Add(4);

   foreach (int i in il.BidirectionalSubrange(false, 1, 3))
    Console.WriteLine(i);
  }
 }
}
