using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_22___Delegates
{
    class lambdas
    {
        List<int> m_list = new List<int>();

        public void Simplest()
        {
            m_list.Select((int i) => { return i + 1; });
        }

        public void ImplicitParameter()
        {
            m_list.Select((i) => { return i + 1; });
        }

        public void ImplicitParameterError()
        {
            Func<int, int> incrementLambda = (int i) => { return i + 1; };
        }

        public void FindIndex()
        {
            List<Employee> employees = new List<Employee>();

            int index = employees.FindIndex(e => e.Age > 40);
        }
    }
}
