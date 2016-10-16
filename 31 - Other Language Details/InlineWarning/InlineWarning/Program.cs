#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

class Program
{
 static void Main(string[] args)
 {
  //Use the original DoStuff method (now marked Obsolete) until the bug 
  //in new version that is documented in vendor KB article 123 is fixed.
#pragma warning disable 612
  DoStuff();
#pragma warning restore 612
 }

 [Obsolete]
 static void DoStuff()
 {
  ;
 }
}
