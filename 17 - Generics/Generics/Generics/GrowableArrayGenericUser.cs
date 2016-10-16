#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Generics
{
 public class GrowableArrayGenericUser
 {
  public GrowableArrayGenericUser()
  {
   GrowableArray<int> ga = new GrowableArray<int>();
   int num = 10;
   ga.AddElement(num); //no boxing operation here
   int newNum = (int)ga.GetElement(0); //type safety and no unboxing operation
   // string str = (string)ga.GetElement(0); //would not compile
  }

  public void MethodOne(GrowableArray<double> gad)
  {
   ;
  }

  public void MethodTwo(GrowableArray<object> gad)
  {
   ;
  }

  static void main()
  {
   GrowableArray<int> ga = new GrowableArray<int>();
   //MethodOne(ga);  //wil NOT compile
   //MethodTwo(ga);  //wil NOT compile
  }

  static void main2()
  {
   GrowableArray<double> ga = new GrowableArray<double>();
   int i = 7;
   ga.AddElement(i);
  }
 }
}
