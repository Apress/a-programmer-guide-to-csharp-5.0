#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

class ProgramAM
{
 static void MainAM(string[] args)
 {
  AppDomain.CurrentDomain.CatchingException +=
  delegate
  {
   Console.WriteLine("An exception is being caught in the app domain");
  };
 }


}
