#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace NullableTypes
{
 public class GenericType
 {
  public static void DoStuff()
  {
   Nullable<int> i = null;
   bool b = i.HasValue; //will be false
   int j = Nullable.GetValueOrDefault<int>(i); //returns 0
   //j = i.Value; //would throw an exception
   //j = (int)i; //would throw an exception
   i = 123;
   j = Nullable.GetValueOrDefault<int>(i); //returns 123
   j = (int)123; //works fine
  }

  public static void LanguageNullables()
  {
   int? i = null; //same as Nullable<int> i = null;
   int? j = 0; // same as Nullable<int> j = 0;

   bool isNotNull = i.HasValue; //returns false, same as 
  }
 }
}
