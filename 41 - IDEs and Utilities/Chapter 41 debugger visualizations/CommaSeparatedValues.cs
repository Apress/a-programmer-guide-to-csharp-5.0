using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//[DebuggerDisplay("({m_values.Length}) {ToString()}")]
[DebuggerDisplay("{DebuggerDisplay,nq}")]
//[DebuggerTypeProxy(typeof(CommaSeparatedValuesDebuggerProxy))]
class CommaSeparatedValues
{
    public CommaSeparatedValues(string valuesAsString)
    {
        m_values = valuesAsString.Split(',');
    }
    public override string ToString()
    {
        return String.Join(",", m_values);
    }
    public string DebuggerDisplay
    {
        get
        {
            return String.Format(@"({0}) ""{1}""",
                m_values.Length,
                ToString());
        }
    }

    //[DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    string[] m_values;

    class CommaSeparatedValuesDebuggerProxy
    {
        CommaSeparatedValues m_commaSeparatedValues;

        public CommaSeparatedValuesDebuggerProxy(CommaSeparatedValues commaSeparatedValues)
        {
            m_commaSeparatedValues = commaSeparatedValues;
        }

        public int Count
        {
            get { return m_commaSeparatedValues.m_values.Length; }
        }

        public string FirstItem
        {
            get
            {
                return m_commaSeparatedValues.m_values.Length != 0 ?
                    m_commaSeparatedValues.m_values[0] : null;
            }
        }
        public string[] Items { get { return m_commaSeparatedValues.m_values; } }
    }
}

