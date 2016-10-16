using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_18_indexers
{
public class IntList: IEnumerable<int>
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
        get { return m_values[index]; }
        set { m_values[index] = value; }
    }
    public int Count { get { return m_count; } }
    public IEnumerator<int> GetEnumerator()
    {
        return new IntListEnumerator(this);
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
public class IntListEnumerator : IEnumerator<int>
{
    IntList m_intList;
    int m_index;
    internal IntListEnumerator(IntList intList)
    {
        m_intList = intList;
        Reset();
    }
    public bool MoveNext()
    {
        m_index++;
        return m_index < m_intList.Count;
    }
    public int Current
    {
        get { return (m_intList[m_index]); }
    }
    object IEnumerator.Current
    {
        get { return Current; }
    }
    void IDisposable.Dispose()
    {
    }
    public void Reset()
    {
        m_index = -1;
    }
}
}
