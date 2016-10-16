using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_30___Linq_to_SQL
{
    public class LinqJoin
    {
        public static void JoinExample()
        {
            AdventureWorksDataContext context = new AdventureWorksDataContext();

var salesByPerson = 
    context.Persons.Join(context.SalesPersons,
                         person => person.BusinessEntityID,
                         salesPerson => salesPerson.BusinessEntityID,
                        (person, salesPerson) => new 
                        { 
                            FirstName = person.FirstName, 
                            SalesLastYear = salesPerson.SalesLastYear
                        } );

            foreach (var value in salesByPerson)
            {
                Console.WriteLine("{0}: {1}", value.FirstName, value.SalesLastYear);
            }
        }

        public static void JoinExampleSqlSyntax()
        {
            AdventureWorksDataContext context = new AdventureWorksDataContext();

var salesByPerson = 
    from person in context.Persons 
    join salesPerson in context.SalesPersons
    on person.BusinessEntityID equals salesPerson.BusinessEntityID
    select new 
    { 
        FirstName = person.FirstName, 
        SalesLastYear = salesPerson.SalesLastYear
    };

            foreach (var value in salesByPerson)
            {
                Console.WriteLine("{0}: {1}", value.FirstName, value.SalesLastYear);
            }
        }
    }
}
