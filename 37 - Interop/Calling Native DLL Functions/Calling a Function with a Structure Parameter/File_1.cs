// 31 - Interop\Calling Native DLL Functions\Calling a Function with a Structure Parameter
// copyright 2000 Eric Gunnerson
using System;
using System.Runtime.InteropServices;

struct Point
{
    public int x;
    public int y;
    
    public override string ToString()
    {
        return(String.Format("({0}, {1})", x, y));
    }
}

struct Rect
{
    public int left;
    public int top;
    public int right;
    public int bottom;
    
    public override string ToString()
    {
        return(String.Format("({0}, {1})\n    ({2}, {3})", left, top, right, bottom));
    }
}

struct WindowPlacement
{
    public uint length;
    public uint flags;
    public uint showCmd;
    public Point minPosition;
    public Point maxPosition;
    public Rect normalPosition;    
    
    public override string ToString()
    {
        return(String.Format("min, max, normal:\n{0}\n{1}\n{2}",
        minPosition, maxPosition, normalPosition));
    }
}

class Window
{
    [DllImport("user32")]
    static extern IntPtr GetForegroundWindow();
    
    [DllImport("user32")]
    static extern bool GetWindowPlacement(IntPtr handle, ref WindowPlacement wp);
    
    public static void Main()
    {
        IntPtr window = GetForegroundWindow();
        
        WindowPlacement wp = new WindowPlacement();
        wp.length = (uint) Marshal.SizeOf(wp);
        
        bool result = GetWindowPlacement(window, ref wp);
        
        if (result)
        {
            Console.WriteLine(wp);
        }
    } 
}