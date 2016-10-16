using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_30___Linq_to_SQL
{
    class LinqStoredProcedure
    {
public static void OutputManagerEmployees(int managerId)
{
    AdventureWorksDataContext context = new AdventureWorksDataContext();

    var employees = context.uspGetManagerEmployees(managerId);
    foreach (var employee in employees)
    {
        Console.WriteLine(employee.FirstName);
    }

    var mark = employees.Where(emp => emp.FirstName == "Mark").First();
    Console.WriteLine(mark.LastName);
}

public static void OutputManagerEmployeesAndFilter(int managerId)
{
    AdventureWorksDataContext context = new AdventureWorksDataContext();
    var fred = context.uspGetManagerEmployees(managerId);

    var mark = context.uspGetManagerEmployees(managerId)
                .Where(emp => emp.FirstName == "Mark")
                .First();
    Console.WriteLine(mark.LastName);
}

    }
}
