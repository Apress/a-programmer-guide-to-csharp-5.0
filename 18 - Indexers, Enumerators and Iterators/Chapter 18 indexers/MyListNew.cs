using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_18_indexers
{
public class MyListNew<T> : IEnumerable<T>
{
    int m_count = 0;
    T[] m_values;
    public MyListNew(int capacity)
    {
        m_values = new T[capacity];
    }
    public void Add(T value)
    {
        m_values[m_count] = value;
        m_count++;
    }
    public T this[int index]
    {
        get { return m_values[index]; }
        set { m_values[index] = value; }
    }
    public int Count { get { return m_count; } }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public IEnumerator<T> GetEnumerator()
    {
        for (int index = 0; index < m_count; index++)
        {
            yield return m_values[index];
        }
    }
    public IEnumerable<T> ReversedItems()
    {
        for (int index = m_count - 1; index >= 0; index--)
        {
            yield return m_values[index];
        }
    }
}
}
