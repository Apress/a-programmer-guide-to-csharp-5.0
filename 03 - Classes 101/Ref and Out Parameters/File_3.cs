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
    
    public void GetPoint(out int x, out int y)
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
        int x;
        int y;
        
        myPoint.GetPoint(out x, out y);
        Console.WriteLine("myPoint({0}, {1})", x, y);
    }
}