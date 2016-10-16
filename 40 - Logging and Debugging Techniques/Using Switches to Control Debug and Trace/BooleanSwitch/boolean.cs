// 37 - Defensive Programming\Using Switches to Control Debug and Trace\BooleanSwitch
// copyright 2000 Eric Gunnerson
// file=boolean.cs
// compile with: csc /D:DEBUG /r:system.dll boolean.cs
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
        Debug.WriteLineIf(debugOutput.Enabled, "VerifyState Start");
        
        if (debugOutput.Enabled)
        Debug.WriteLine("VerifyState End");
    }
    
    BooleanSwitch    debugOutput = 
    new BooleanSwitch("MyClassDebugOutput", "Control debug output");
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