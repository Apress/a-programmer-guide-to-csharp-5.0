#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime;
#endregion

class Program
{
 static void Main(string[] args)
 {
  OuterVariables o = new OuterVariables();
  AppDomain.CurrentDomain.CatchingException += new CatchingExceptionEventHandler(CurrentDomain_CatchingException);
 }

 static void CurrentDomain_CatchingException(object sender, CatchingExceptionEventArgs e)
 {
  Console.WriteLine("An exception is being caught in the app domain");
 }
}
