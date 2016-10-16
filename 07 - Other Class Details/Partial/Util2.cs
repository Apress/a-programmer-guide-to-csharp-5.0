#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace OtherClassDetails
{
 partial class Util
 {
 }

 public class MyBase{}

 partial class MyPartialClass : MyBase 
 { 
  public void Dispose() { }
 }

 partial class MyPartialClass : IDisposable { }

 partial struct MyInt { }
}
