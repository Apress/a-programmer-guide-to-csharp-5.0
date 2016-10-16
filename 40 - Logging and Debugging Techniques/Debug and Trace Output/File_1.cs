// 37 - Defensive Programming\Debug and Trace Output
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
        Debug.WriteLineIf(debugOutput, "In VerifyState");
        Debug.Assert(i == 0, "Bad State");
    }
    
    static public bool DebugOutput
    {
        get
        {
            return(debugOutput);
        }
        set
        {
            debugOutput = value;
        }
    }
    
    int i = 0;
    static bool debugOutput = false;
}

class Test
{
    public static void Main()
    {
        Debug.Listeners.Clear();
        Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
        MyClass c = new MyClass(1);
        
        c.VerifyState();
        MyClass.DebugOutput = true;
        c.VerifyState();
    }
}