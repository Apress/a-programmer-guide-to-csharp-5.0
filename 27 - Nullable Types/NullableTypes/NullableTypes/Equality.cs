#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace NullableTypes
{
 public class Equality
 {
  public Equality()
  {
   int? i = null;
   int? j = null;
   bool b = i == j;
  }
 }
}
