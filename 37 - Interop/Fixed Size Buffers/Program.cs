using System;
using System.Runtime.InteropServices;

namespace Interop
{
  class Program
 {
  [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
   static extern bool GetVersionEx(ref OSVERSIONINFO lpVersionInfo);

  unsafe static void Main(string[] args)
  {
   OSVERSIONINFO versionInfo = new OSVERSIONINFO();
   versionInfo.dwOSVersionInfoSize = (uint)sizeof(OSVERSIONINFO);
   bool res = GetVersionEx(ref versionInfo);
   Console.WriteLine(Marshal.PtrToStringUni(new IntPtr(versionInfo.szCSDVersion)));
  }
 }


 unsafe struct OSVERSIONINFO
 {
  public uint dwOSVersionInfoSize;  
  public uint dwMajorVersion;  
  public uint dwMinorVersion;  
  public uint dwBuildNumber;
  public uint dwPlatformId; 
  public fixed char szCSDVersion[128];
 }
}
