using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
 class MyGenericClass<T> { }

 class Reflection
 {
  public Reflection()
  {
   List<int> l = new List<int>();
   bool b1 = l.GetType().IsGenericTypeDefinition;
   bool b2 = l.GetType().GetGenericTypeDefinition().IsGenericTypeDefinition;

   DumpGenericTypeParams(typeof(MyGenericClass<int>).GetGenericTypeDefinition());
  }

  static void DumpGenericTypeParams(Type t)
  {
   if (t.IsGenericTypeDefinition)
   {
    foreach (Type genericType in t.GetGenericArguments())
    {
     Console.WriteLine(genericType.Name);
    }
   }
  }
 }
}
