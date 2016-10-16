// 08 - Other Class Details\Creation, Initialization, Destruction\Constructors
// copyright 2000 Eric Gunnerson
using System;
class MyObject
{
    public MyObject(int x)
    {
        this.x = x;
    }
    public MyObject(int x, int y): this(x)
    {
        this.y = y;
    }
    public int X
    {
        get
        {
            return(x);
        }
    }
    public int Y
    {
        get
        {
            return(y);
        }
    }
    int x;
    int y;
}
class Test
{
    public static void Main()
    {
        MyObject my = new MyObject(10, 20);
        Console.WriteLine("x = {0}, y = {1}", my.X, my.Y);
    }
}