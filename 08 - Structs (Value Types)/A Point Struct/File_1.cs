// 09 - Structs (Value Types)\A Point Struct
// copyright 2000 Eric Gunnerson
using System;
struct Point
{
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public override string ToString()
    {
        return(String.Format("({0}, {1})", x, y));
    }
    
    public int x;
    public int y;
}
class Test
{
    public static void Main()
    {
        Point    start = new Point(5, 5);
        Console.WriteLine("Start: {0}", start);
    }
}