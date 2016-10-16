using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

struct Point
{
    public int m_x;
    public int m_y;

    public Point(int x, int y)
    {
        m_x = x;
        m_y = y;
    }
    public override string ToString()
    {
        return String.Format("({0}, {1})", m_x, m_y);
    }
}
