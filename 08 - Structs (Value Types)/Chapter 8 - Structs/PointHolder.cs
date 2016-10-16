using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_8___Structs
{
class PointHolder
{
    public PointHolder(Point point)
    {
        Current = point;
    }

    public Point Current;
}
}
