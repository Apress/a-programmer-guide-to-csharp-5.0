// 07 - Member Accessibility and Overloading\Method Overloading\Method Hiding
// copyright 2000 Eric Gunnerson
using System;
public class Base
{
    public void Process(short value)
    {
        Console.WriteLine("Base.Process(short): {0}", value);
    }
}
public class Derived: Base
{
    public void Process(int value)
    {
        Console.WriteLine("Derived.Process(int): {0}", value);
    }
    
    public void Process(string value)
    {
        Console.WriteLine("Derived.Process(string): {0}", value);
    }
}
class Test
{
    public static void Main()
    {
        Derived d = new Derived();
        short i = 12;
        d.Process(i);
        ((Base) d).Process(i);
    }
}