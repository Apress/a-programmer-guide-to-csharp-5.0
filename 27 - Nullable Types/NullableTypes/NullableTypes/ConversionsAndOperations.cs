#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace NullableTypes
{
 public class ConversionsAndOperations
 {
  public ConversionsAndOperations()
  {
   int? i = 1;
   int? j = 2;
   int? k = i + j;  //addition operator works fine
   double? d = k; //no problem with implicit conversion to double
   short? s = (short?)d; //explicit cast required to short? becuase of possible data loss
  }

  public static void ConversionsAndOperationsWithNull()
  {
   int? i = 1;
   int? h = 2;
   int? j = null;
   int? k = i + j + h + 3;
   double? d = k;
   short? s = (short?)d;
  }
 }
}
