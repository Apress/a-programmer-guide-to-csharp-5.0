using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_33___arrays
{

class CompareEmployeeByName : IEqualityComparer<Employee>
{
    public bool Equals(Employee emp1, Employee emp2)
    {
        return String.Compare(emp1.Name, emp2.Name) == 0;
    }

    public int GetHashCode(Employee emp1)
    {
        return emp1.Name.GetHashCode();
    }
}
class Testy
{
    public static void Mainly()
    {
        Employee herb = new Employee("Herb", 555);
        Employee george = new Employee("George", 123);
        Employee frank = new Employee("Frank", 111);
        Dictionary<Employee, string> employeeAddresses = 
            new Dictionary<Employee, string>(new CompareEmployeeByName());

        employeeAddresses.Add(herb, "414 Evergreen Terrace");
        employeeAddresses.Add(george, "2335 Elm Street");
        employeeAddresses.Add(frank, "18 Pine Bluff Road");
        Employee herbClone = new Employee("Herb", 000);
        string address = employeeAddresses[herbClone];
        Console.WriteLine("{0} lives at {1}", herbClone, address);
    }
}

}
