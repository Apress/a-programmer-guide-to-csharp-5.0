using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_18_indexers
{
public class IntListNew: IEnumerable
{
    int m_count = 0;
    int[] m_values;
    public IntListNew(int capacity)
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
        get { return m_values[index]; }
        set { m_values[index] = value; }
    }
    public int Count { get { return m_count; } }
    public IEnumerator GetEnumerator()
    {
        for (int index = 0; index < m_count; index++)
        {
            yield return m_values[index];
        }
    }
public IEnumerable ReversedItems()
{
    for (int index = m_count - 1; index >= 0; index--)
    {
        yield return m_values[index];
    }
}
}
}