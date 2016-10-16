#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Generics
{
 public interface IGeneric<T>
 {
  T InterfaceMethod();
 }

 public class GenericInterfaceImplementor<T>: IGeneric<T>
 {
  public T InterfaceMethod() { return default(T);}
 } 

 public class InterfaceImplementor: IGeneric<int>
 {
  public int InterfaceMethod() { return 0; }
 }

 /* compile error CS0111: Type 'DerivedInterfaceImplementor' already defines a member called 
 'InterfaceMethod' with the same parameter types

 public class DerivedInterfaceImplementor : InterfaceImplementor, IGeneric<double>
 {
  public int InterfaceMethod() { return 0; }
  public double InterfaceMethod() { return 0.0; }
 }
 */
}