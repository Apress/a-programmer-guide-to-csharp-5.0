using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_37_Interop
{
class CallMessageBox
{
    [DllImport("user32.dll")]
    static extern int MessageBox(IntPtr h, string m, string c, int type);
    
    public static void ShowMessageBox(string message)
    {
        int retval = MessageBox(IntPtr.Zero, message, "Caption", 0);
    }
}
}
