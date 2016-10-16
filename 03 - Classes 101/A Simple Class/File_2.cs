// 05 - Classes 101\A Simple Class
// copyright 2000 Eric Gunnerson
using System;
class Point
{
    // constructor
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    // member fields
    public int x;
    public int y;
}

class Test
{
    public static void Main()
    {
        Point myPoint = new Point(10, 15);
        Console.WriteLine("myPoint.x {0}", myPoint.x);
        Console.WriteLine("myPoint.y {0}", myPoint.y);
    }
}