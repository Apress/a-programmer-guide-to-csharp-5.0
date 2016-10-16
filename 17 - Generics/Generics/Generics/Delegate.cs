#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Generics3
{
 public class GenericClass <T>
 {
  public delegate T GenericDelegate(int i);

  public void UseDelegate(GenericDelegate del) {}
 }

 public class NonGeneric
 {
  public delegate T GenericDelegate1<T>(int i) where T : class;
  public delegate U GenericDelegate2<U, V>(V v);

  public void UseDelegate<T>(GenericDelegate1<T> del) where T : class { }
 }

 public class GenericDelegateUser
 {
  string StandardMethod(int i) { return ""; }

  public void UseGenericDelegates()
  {
   GenericClass<string> gcs = new GenericClass<string>();

   //generic method can be called used explicitly like this
   GenericClass<string>.GenericDelegate del = new GenericClass<string>.GenericDelegate(StandardMethod);
   gcs.UseDelegate(del);

   //or implicity like this
   gcs.UseDelegate(StandardMethod);

   //Implicit use of generic delegate
   NonGeneric ng = new NonGeneric();
   ng.UseDelegate<string>(StandardMethod);
  }
 }

}
