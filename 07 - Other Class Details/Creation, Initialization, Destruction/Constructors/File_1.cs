// 08 - Other Class Details\Creation, Initialization, Destruction\Constructors
// copyright 2000 Eric Gunnerson
using System;
public class BaseClass
{
    public BaseClass(int x)
    {
        this.x = x;
    }
    public int X
    {
        get
        {
            return(x);
        }
    }
    int    x;
}
public class Derived: BaseClass
{
    public Derived(int x): base(x)
    {
    }
}
class Test
{
    public static void Main()
    {
        Derived d = new Derived(15);
        Console.WriteLine("X = {0}", d.X);
    }
}