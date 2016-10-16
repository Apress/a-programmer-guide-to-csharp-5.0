using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_12___Variable_Scoping_and_Definite_Assignment
{
    class MyClass
    {
        public MyClass(int value)
        {
            m_value = value;
        }
        public int Calculate()
        {
            return (m_value * 10);
        }
        public int m_value;
    }
    class Test
    {
        public static void Mainly()
        {
            MyClass mine;

            //Console.WriteLine("{0}", mine.m_value);        // error
            //Console.WriteLine("{0}", mine.Calculate());    // error
            mine = new MyClass(12);
            Console.WriteLine("{0}", mine.m_value);        // okay now…
        }
    }

}
