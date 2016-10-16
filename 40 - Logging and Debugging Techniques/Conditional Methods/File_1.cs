// 37 - Defensive Programming\Conditional Methods
// copyright 2000 Eric Gunnerson
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
        if (i != 0)
        Console.WriteLine("Bad State");
    }
    
    int i = 0;
}

class Test
{
    public static void Main()
    {
        MyClass c = new MyClass(1);
        
        c.VerifyState();
    }
}