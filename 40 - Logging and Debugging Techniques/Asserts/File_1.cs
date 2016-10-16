// 37 - Defensive Programming\Asserts
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
        Debug.Assert(i == 0, "Bad State");
    }
    
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