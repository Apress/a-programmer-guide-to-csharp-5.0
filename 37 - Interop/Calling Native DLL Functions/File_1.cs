// 31 - Interop\Calling Native DLL Functions
// copyright 2000 Eric Gunnerson
using System;
using System.Runtime.InteropServices;
class Test
{
    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr h, string m, 
    string c, int type);
    public static void Main()
    {
        int retval = MessageBox(IntPtr.Zero, "Hello", "Caption", 0);
    }
}