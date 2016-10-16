using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_16___Generic_Types
{
    class MySortedList<T> where T: IComparable
    {
        int m_count = 0;
        T[] m_values;

        public MySortedList(int capacity)
        {
            m_values = new T[capacity];
        }
        public void Add(T value)
        {
            m_values[m_count] = value;
            m_count++;
        }
        void Sort()
        {
            // You shouldn't write your own sort routines, so I won't write one here.
            // I will include one comparison, so that a constraint is required. 
            int x = 0;
            int y = 1;
            if (m_values[x].CompareTo(m_values[y]) > 0)
            {
            }
        }
        public T this[int index]
        {
            get { return m_values[index]; }
            set { m_values[index] = value; }
        }
        public int Count { get { return m_count; } }
    }
}
