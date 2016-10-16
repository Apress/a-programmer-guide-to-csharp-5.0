// 09 - Structs (Value Types)\Structs and Constructors
// copyright 2000 Eric Gunnerson
using System;
struct Point
{
    int x;
    int y;
    
    Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public override string ToString()
    {
        return(String.Format("({0}, {1})", x, y));
    }
}
class Test
{
    public static void Main()
    {
        Point[] points = new Point[5];
        Console.WriteLine("[2] = {0}", points[2]);
    }
}