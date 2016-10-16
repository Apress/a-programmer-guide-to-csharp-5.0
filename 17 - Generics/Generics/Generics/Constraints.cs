#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Generics
{
 public interface IAddable
 {
  void Add();
 }

 public sealed class A { }

 public class Container<T, K> 
  where T: class, IConvertible, new() 
  where K: struct
 {
  public Container()
  {
   T t = new T();
   int i = t.ToInt32(null);
   K k = new K();
  }

  
 }

 public class Utils2
 {
  public static T GetBiggest<T>(T t1, T t2) where T : IComparable
  {
   return t1.CompareTo(t2) > 0 ? t1 : t2;
  }

  public static void Compare()
  {
   string s = string.Format("Out of 1 and 2, {0} is bigger", GetBiggest(1, 2));
   Console.WriteLine(s);
  }
 }

}
