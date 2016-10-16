#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Generics
{
 public class Utils
 {
  Random _random = new Random();

  public T ReturnRandomElementOfArray<T>(T[] coll)
  {
   return coll[_random.Next(coll.Length - 1)];
  }

  public int ReturnRandomElementOfArray(int[] coll)
  {
   return coll[_random.Next(coll.Length - 1)];
  }

  public static void UtilCaller()
  {
   Utils u = new Utils();
   double[] dblColl = new double[]{1.0, 2.0, 3.0};
   int[] intColl = new int[]{4, 5, 6};
   double dblRandom = u.ReturnRandomElementOfArray<double>(dblColl);  //type argument specified
   int intRandom = u.ReturnRandomElementOfArray<int>(intColl); //type argument infered
  }
 }
}
