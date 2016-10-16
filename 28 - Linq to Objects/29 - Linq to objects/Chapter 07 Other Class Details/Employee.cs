using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_07_Other_Class_Details
{
public class Employee
{
    public Employee()
    {
    }

public Employee(string name, int age, decimal salary)
{
    Name = name;
    Age = age;
    Salary = salary;
}

    public string Name { get; set; }
    public int Age { get; set; }
    public decimal Salary { get; set; }
}
}
