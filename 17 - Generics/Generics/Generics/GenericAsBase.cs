#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Generics
{
 //public class GenericAsBase<T> : T {}

 //base
 public class BaseClass { }
 //first tier
 public class DerivedGenericClass<T>: BaseClass {}
 //second tier
 public class DerivedClass: DerivedGenericClass<int> {}
 public class SecondDerivedGenericClass <T>: DerivedGenericClass<T> { }

 //constrainst repeated in derived generic class
 public class GenericBase<T> where T : new() {}
 public class GenericDerived<T> : GenericBase<T> where T : new() { }

 //type arguments
 public class Map<T, K> { }
 public class StringMap<K> : Map<string, K> { }
 public class StringToIntMap : StringMap<int> { }
 public class StringToDoubleMap : Map<string, double> { }
}
