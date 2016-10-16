using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.Runtime.InteropServices;

struct WindowPlacement
{
    public uint length;
    public uint flags;
    public uint showCmd;
    public Point minPosition;
    public Point maxPosition;
    public Rectangle normalPosition;

    public override string ToString()
    {
        return String.Format("min, max, normal:\n{0}\n{1}\n{2}",
            minPosition, maxPosition, normalPosition);
    }
}
class Window
{
    [DllImport("user32")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32")]
    static extern bool GetWindowPlacement(IntPtr handle, ref WindowPlacement wp);

    public static void Mainly()
    {
        IntPtr window = GetForegroundWindow();

        WindowPlacement wp = new WindowPlacement();
        wp.length = (uint)Marshal.SizeOf(wp);

        bool result = GetWindowPlacement(window, ref wp);

        if (result)
        {
            Console.WriteLine(wp);
        }
    }
}
