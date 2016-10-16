#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace NullableTypes
{
 public class Bitwise
 {
  public Bitwise()
  {
   bool? i = true;
   bool? j = false;
   bool? k = null;

   bool? res1 = i | k; //returns true, NOT null
   bool? res2 = j & k; //returns false, NOT NULL
  }
 }
}
