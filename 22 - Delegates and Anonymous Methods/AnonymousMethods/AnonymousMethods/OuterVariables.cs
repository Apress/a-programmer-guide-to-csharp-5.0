#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

public class OuterVariables
{
 public OuterVariables()
 {
  int sum = 0;
  int[] arr = new int[] { 1, 2, 3 };
  Array.ForEach(arr, delegate(int i) { sum += i; });
  Console.WriteLine(sum);
 }
}
