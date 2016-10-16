#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace NullableTypes
{
 class Program
 {
  static void Main(string[] args)
  {
   GenericType.DoStuff();
   ConversionsAndOperations.ConversionsAndOperationsWithNull();
   Equality e = new Equality();
   Bitwise b = new Bitwise();
   Coalescing c = new Coalescing();
  }
 }
}
