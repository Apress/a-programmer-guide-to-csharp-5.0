using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_33___arrays
{
public class Employee : IComparable<Employee>
{
    public Employee(string name, int id)
    {
        m_name = name;
        m_id = id;
    }
    public string Name
    { 
        get { return m_name; } 
    }
    int IComparable<Employee>.CompareTo(Employee emp2)
    {
        if (m_id > emp2.m_id)
        {
            return 1;
        }
        else if (m_id < emp2.m_id)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    public override string ToString()
    {
        return String.Format("{0}:{1}", m_name, m_id);
    }
    string m_name;
    int m_id;
}
class Test
{
    public static void Mainly()
    {
        List<Employee> employees = new List<Employee>();
        employees.Add(new Employee("George", 1));
        employees.Add(new Employee("Fred", 2));
        employees.Add(new Employee("Tom", 4));
        employees.Add(new Employee("Bob", 3));

        employees.Sort();
        foreach (Employee employee in employees)
        {
            Console.WriteLine("Employee: {0}", employee);
        }
        // Find employee id 2 in the list;
        Employee employeeToFind = new Employee(null, 2);
        int index = employees.BinarySearch(employeeToFind);
        if (index != -1)
        {
            Console.WriteLine("Found: {0}", employees[index]);
        }
    }
}
}
