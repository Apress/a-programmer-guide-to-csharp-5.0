    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_22___Delegates
{
    class Variables
    {
        public static void Example()
        {
List<Employee> employees = new List<Employee>();
employees.Add(new Employee("John", 33, 22000m));
employees.Add(new Employee("Eric", 42, 18000m));
employees.Add(new Employee("Michael", 33, 19500m));

int ageThreshold = 40;
Predicate<Employee> match = e => e.Age > ageThreshold;
ageThreshold = 30;

int index = employees.FindIndex(match);
Console.WriteLine(index);
Console.WriteLine(ageThreshold);
        }

    }
}
