// 15 - Conversions\Conversions of Classes (Reference Types)\To the Base Class of an Object
// copyright 2000 Eric Gunnerson
using System;
public class Base
{
    public virtual void WhoAmI()
    {
        Console.WriteLine("Base");
    }
}
public class Derived: Base
{
    public override void WhoAmI()
    {
        Console.WriteLine("Derived");
    }
}
public class Test
{
    public static void Main()
    {
        Derived d = new Derived();
        Base b = d;
        
        b.WhoAmI();
        Derived d2 = (Derived) b;
        
        object o = d;
        Derived d3 = (Derived) o;
    }
}