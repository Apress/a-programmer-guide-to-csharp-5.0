using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_8___Structs
{
    class Program
    {
static void Main(string[] args)
{
    Example();
}

static void Example()
{
    Point point = new Point(10, 15);
    PointHolder pointHolder = new PointHolder(point);

    Console.WriteLine(pointHolder.Current);

    Point current = pointHolder.Current;
    current.m_x = 500;

    Console.WriteLine(pointHolder.Current);
}
    }
}
