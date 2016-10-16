using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_30___Linq_to_SQL
{
    class Linq
    {
        public static void PersonExample()
        {
AdventureWorksDataContext context = new AdventureWorksDataContext();

var employeeLastNames = context.Persons
                    .Where(person => person.PersonType == "EM")
                    .Select(person => person.LastName);

foreach (var lastName in employeeLastNames)
{
    Console.WriteLine(lastName);
}
        }

        public static void PersonExampleSqlSyntax()
        {
            AdventureWorksDataContext context = new AdventureWorksDataContext();

var employeeLastNames =
    from person in context.Persons
    where person.PersonType == "EM"
    select person.LastName;

Console.WriteLine(context.GetCommand(employeeLastNames).CommandText);

        foreach (var lastName in employeeLastNames)
            {
                Console.WriteLine(lastName);
            }
        }
    }
}
