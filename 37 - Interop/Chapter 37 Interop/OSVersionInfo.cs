// comment this out to enable the automatically-marshalled version.
#define fixed

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

unsafe struct OsVersionInfo
{
    public uint OsVersionInfoSize;  
    public uint MajorVersion;  
    public uint MinorVersion;  
    public uint BuildNumber;
    public uint PlatformId; 
#if fixed
    public fixed byte ServicePackVersion[128];
#else
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string ServicePackVersion;
#endif
}


class OsVersionInfoTest
{
    [DllImport("Kernel32.dll", /* CharSet = CharSet.Unicode, */ EntryPoint="GetVersionEx")]
    static extern bool GetVersion(ref OsVersionInfo lpVersionInfo);

    unsafe public static void Mainly()
    {
        OsVersionInfo versionInfo = new OsVersionInfo();
        versionInfo.OsVersionInfoSize = (uint) Marshal.SizeOf(versionInfo);
        bool res = GetVersion(ref versionInfo);
#if fixed
        Console.WriteLine(Marshal.PtrToStringAnsi(new IntPtr(versionInfo.ServicePackVersion)));
#else
        Console.WriteLine(versionInfo.ServicePackVersion);
#endif
    }
}



