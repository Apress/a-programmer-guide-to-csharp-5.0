// 31 - Interop\Calling Native DLL Functions\Hooking up to a Windows Callback
// copyright 2000 Eric Gunnerson
using System;
using System.Threading;
using System.Runtime.InteropServices;

class ConsoleCtrl
{
    public enum ConsoleEvent
    {
        CTRL_C = 0,        // From wincom.h
        CTRL_BREAK = 1,
        CTRL_CLOSE = 2,
        CTRL_LOGOFF = 5,
        CTRL_SHUTDOWN = 6
    }
    
    public delegate void ControlEventHandler(ConsoleEvent consoleEvent);
    
    public event ControlEventHandler ControlEvent;
    
    // save delegate so the GC doesn’t collect it.
    ControlEventHandler eventHandler;
    
    public ConsoleCtrl()
    {
        // save this to a private var so the GC doesn't collect it
        eventHandler = new ControlEventHandler(Handler);
        SetConsoleCtrlHandler(eventHandler, true);
    }
    
    private void Handler(ConsoleEvent consoleEvent)
    {
        if (ControlEvent != null)
        ControlEvent(consoleEvent);
    }
    
    [DllImport("kernel32.dll")]
    static extern bool SetConsoleCtrlHandler(ControlEventHandler e, bool add);
}

class Test
{
    public static void MyHandler(ConsoleCtrl.ConsoleEvent consoleEvent)
    {
        Console.WriteLine("Event: {0}", consoleEvent);
    }
    
    public static void Main()
    {
        ConsoleCtrl cc = new ConsoleCtrl();
        cc.ControlEvent += new ConsoleCtrl.ControlEventHandler(MyHandler);
        
        Console.WriteLine("Enter 'E' to exit");        
        
        Thread.Sleep(15000);	// sleep 15 seconds
    }
}