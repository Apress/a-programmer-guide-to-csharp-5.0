using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_27_Nullable_Types
{
struct Integer
{
    int m_value;
    bool m_hasValue;

    public int Value 
    {
        get
        {
            if (!m_hasValue)
            {
                throw new InvalidOperationException("Integer does not have a value");
            }
            return m_value;
        }
        set
        {
            m_value = value;
            m_hasValue = true;
        }
    }
    public bool HasValue
    {
        get { return m_hasValue; }
        set { m_hasValue = value; } 
    }
    public Integer(int value)
    {
        m_value = value;
        m_hasValue = true;
    }
    public static implicit operator Integer(int value)
    {
        return new Integer(value);
    }
    public static Integer Null
    {
        get { return new Integer(); }
    }
    public static implicit operator Integer(string value)
    {
        return new Integer();
    }
}
}
