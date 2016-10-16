using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_33___arrays
{
class SortEmployeeByName : IComparer<Employee>
{
    public int Compare(Employee emp1, Employee emp2)
    {
        return (String.Compare(emp1.Name, emp2.Name));
    }
}

    class TestComparer
    {
        public static void Test()
        {
        List<Employee> employees = new List<Employee>();
        employees.Add(new Employee("George", 1));
        employees.Add(new Employee("Fred", 2));
        employees.Add(new Employee("Tom", 4));
        employees.Add(new Employee("Bob", 3));

        employees.Sort(new SortEmployeeByName());
        foreach (Employee employee in employees)
        {
            Console.WriteLine("Employee: {0}", employee);
        }
        }

        public static void Test2()
        {
        List<Employee> employees = new List<Employee>();
        employees.Add(new Employee("George", 1));
        employees.Add(new Employee("Fred", 2));
        employees.Add(new Employee("Tom", 4));
        employees.Add(new Employee("Bob", 3));

        employees.Sort((a, b) => String.Compare(a.Name, b.Name));
        foreach (Employee employee in employees)
        {
            Console.WriteLine("Employee: {0}", employee);
        }



        }
    }

}
