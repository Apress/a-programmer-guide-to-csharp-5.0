// 05 - Classes 101\Overloading
// copyright 2000 Eric Gunnerson
class Point
{
    // create a new point from x and y values
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    // create a point from an existing point
    public Point(Point p)
    {
        this.x = p.x;
        this.y = p.y;
    }
    
    int x;
    int y;
}

class Test
{
    public static void Main()
    {
        Point myPoint = new Point(10, 15);
        Point mySecondPoint = new Point(myPoint);
    }
}