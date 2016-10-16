using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_32_Making_friends
{
public class Employee: IEquatable<Employee>
{
    public Employee(int id, string name)
    {
        m_id = id;
        m_name = name;
    }
    public override string ToString()
    {
        return m_name + "(" + m_id + ")";
    }
    public bool Equals(Employee other)
    {
        return this == other;
    }
    public override bool Equals(object obj)
    {
        return Equals((Employee)obj);
    }
    public override int GetHashCode()
    {
        return m_id.GetHashCode();
    }
    public static bool operator ==(Employee emp1, Employee emp2)
    {
        if (emp1.m_id != emp2.m_id)
        {
            return false;
        }
        if (emp1.m_name != emp2.m_name)
        {
            return false;
        }
        return true;
    }
    public static bool operator !=(Employee emp1, Employee emp2)
    {
        return !(emp1 == emp2);
    }
    int m_id;
    string m_name;
}
class Test
{

    public static void Mainly()
    {
        Employee herb = new Employee(555, "Herb");
        Employee george = new Employee(123, "George");
        Employee frank = new Employee(111, "Frank");
        Dictionary<Employee, string> employees = new Dictionary<Employee, string>();
        employees.Add(herb, "414 Evergreen Terrace");
        employees.Add(george, "2335 Elm Street");
        employees.Add(frank, "18 Pine Bluff Road");
        Employee herbClone = new Employee(555, "Herb");
        string address = employees[herbClone];
        Console.WriteLine("{0} lives at {1}", herbClone, address);
    }

}

}
