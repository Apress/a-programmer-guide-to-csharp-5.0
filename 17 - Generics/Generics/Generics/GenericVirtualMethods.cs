#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Generics2
{
 //generic base class with a generic method
 public class GenericBase<T>
 {
  public virtual void MyMethodUsingGenericParameter(T t) { }
  public virtual void MyGenericMethod<W>(W w) where W: IComparable{ }
 }

 //derived generic class
 public class GenericInherited<V>: GenericBase<V>
 {
  public override void MyMethodUsingGenericParameter(V v) { }
  public override void MyGenericMethod<W>(W w) { w.CompareTo(null); }
 }

 //non-generic class
 public class NonGenericInherited : GenericInherited<int>
 {
  public override void MyMethodUsingGenericParameter(int i) { }
  public override void MyGenericMethod<W>(W w) {}
 }
}
