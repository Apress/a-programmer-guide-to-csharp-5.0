// 05 - Classes 101\Ref and Out Parameters
// copyright 2000 Eric Gunnerson
// error
using System;
class Point
{
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    // get both values in one function call
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
        int x;
        int y;
        
        // illegal 
        myPoint.GetPoint(ref x, ref y);
        Console.WriteLine("myPoint({0}, {1})", x, y);
    }
}