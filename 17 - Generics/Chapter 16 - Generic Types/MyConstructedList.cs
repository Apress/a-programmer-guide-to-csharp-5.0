using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_16___Generic_Types
{
    class MyConstructedList<T> where T : new()
    {
        int m_count = 0;
        T[] m_values;

public MyConstructedList(int capacity)
{
    m_values = new T[capacity];
    for (int i = 0; i < capacity; i++)
    {
        m_values[i] = new T();
    }
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
    }
}
