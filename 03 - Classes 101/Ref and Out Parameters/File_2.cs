// 05 - Classes 101\Ref and Out Parameters
// copyright 2000 Eric Gunnerson
using System;
class Point
{
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public void GetPoint(ref int x, ref int y)
    {
        x = this.x;
        y = this.y;
    }
    
    int x;
    int y;
}

class Test
{
    public static void Main()
    {
        Point myPoint = new Point(10, 15);
        int x = 0;
        int y = 0;
        
        myPoint.GetPoint(ref x, ref y);
        Console.WriteLine("myPoint({0}, {1})", x, y);
    }
}