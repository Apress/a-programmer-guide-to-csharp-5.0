using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_16___Generic_Types
{
public class IntList
{
    int m_count = 0;
    int[] m_values;

    public IntList(int capacity)
    {
        m_values = new int[capacity];
    }
    public void Add(int value)
    {
        m_values[m_count] = value;
        m_count++;
    }
    public int this[int index]
    {
        get { return m_values[index];}
        set { m_values[index] = value; }
    }
    public int Count { get { return m_count; } }
}
}
