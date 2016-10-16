using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_22___Delegates
{
class EmployeeFilterByAge
{
    int m_age;

    public EmployeeFilterByAge(int age)
    {
        m_age = age;
    }

    public bool OlderThan(Employee employee)
    {
        return employee.Age > m_age;
    }
}
    
    class FilterByAge
    {
        public static void Example()
        {
List<Employee> employees = new List<Employee>();
employees.Add(new Employee("John", 33, 22000m));
employees.Add(new Employee("Eric", 42, 18000m));
employees.Add(new Employee("Michael", 33, 19500m));

EmployeeFilterByAge filterByAge = new EmployeeFilterByAge(40);
int index = employees.FindIndex(filterByAge.OlderThan);
        }
    }
}
