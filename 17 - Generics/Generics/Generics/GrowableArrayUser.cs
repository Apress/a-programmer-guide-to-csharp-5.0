#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Generics
{
 public class GrowableArrayUser
 {
  public GrowableArrayUser()
  {
   GrowableArray ga = new GrowableArray();
   int num = 10;
   ga.AddElement(num); //boxing operation here
   int newNum = (int)ga.GetElement(0); //no type safety and unboxing operation
   string str = (string)ga.GetElement(0); //runtime error here
  }
 }
}
