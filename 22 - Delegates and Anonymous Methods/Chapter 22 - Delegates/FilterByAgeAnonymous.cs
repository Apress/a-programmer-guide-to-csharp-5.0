using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_22___Delegates
{
    class FilterByAgeAnonymous
    {
        public static void Example()
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee("John", 33, 22000m));
            employees.Add(new Employee("Eric", 42, 18000m));
            employees.Add(new Employee("Michael", 33, 19500m));

int index = employees.FindIndex(
    delegate(Employee employee)
    {
        return employee.Age > 40;
    });
        }
    }
}
