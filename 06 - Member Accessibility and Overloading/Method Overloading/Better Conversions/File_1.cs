// 07 - Member Accessibility and Overloading\Method Overloading\Better Conversions
// copyright 2000 Eric Gunnerson
using System;
public class MyClass
{
    public void Process(long value)
    {
        Console.WriteLine("Process(long): {0}", value);
    }
    public void Process(short value)
    {
        Console.WriteLine("Process(short): {0}", value);
    }
}

class Test
{
    public static void Main()
    {
        MyClass myClass = new MyClass();
        
        int i = 12;
        myClass.Process(i);
        
        sbyte s = 12;
        myClass.Process(s);
    }
}