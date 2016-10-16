using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_16_properties
{
    class Program
    {
        static void Main(string[] args)
        {

        }

string m_name;
public string Name
{
    get { return m_name; }
    set { m_name = value; }
}
    }
}
