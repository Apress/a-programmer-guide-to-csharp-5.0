using System;
using System.Security;
using System.Runtime.InteropServices;

namespace SecureString1
{
 class Program
 {
  static System.Security.SecureString ReadSecretData()
  {
   System.Security.SecureString s = 
    new System.Security.SecureString();
   //read in secret data
   return s;
  }
  static void Main(string[] args)
  {
   using (ReadSecretData())
   {
    //do required processing of data
   }
   //SecureString is now empty


   ToAndFromString();
  }

  //don't do this at home (or work)!!
  static unsafe void ToAndFromString()
  {
   string s = "Some data";
   fixed (char* pS = s)
   {
    using (SecureString ss = new SecureString(pS, s.Length))
    {
     //a few random modifications
     ss.AppendChar('!');
     ss.InsertAt(1, '_');
     ss.SetAt(0, ' ');
     ss.RemoveAt(3);

     //make read-only
     ss.MakeReadOnly();

     //convert back to a string
     IntPtr ssData = Marshal.SecureStringToGlobalAllocUni(ss);
     String newString = Marshal.PtrToStringUni(ssData);
     Marshal.FreeHGlobal(ssData);
    }
   }
  }
 }
}
