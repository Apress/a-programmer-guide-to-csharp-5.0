// 37 - Defensive Programming\Using Switches to Control Debug and Trace\TraceSwitch
// copyright 2000 Eric Gunnerson
// compile with: csc /r:system.dll file_1.cs
using System;
using System.Diagnostics;

class MyClass
{
    public MyClass(int i)
    {
        this.i = i;
    }
    
    [Conditional("DEBUG")]
    public void VerifyState()
    {
        Debug.WriteLineIf(debugOutput.TraceInfo, "VerifyState Start");
        
        Debug.WriteLineIf(debugOutput.TraceVerbose, 
        "Starting field verification");
        
        if (debugOutput.TraceInfo)
        Debug.WriteLine("VerifyState End");
    }
    
    static TraceSwitch    debugOutput = 
    new TraceSwitch("MyClassDebugOutput", "Control debug output");
    int i = 0;
}

class Test
{
    public static void Main()
    {
        Debug.Listeners.Clear();
        Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
        MyClass c = new MyClass(1);
        
        c.VerifyState();
    }
}