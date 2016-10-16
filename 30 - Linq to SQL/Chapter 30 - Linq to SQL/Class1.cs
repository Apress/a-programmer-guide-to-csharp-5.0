using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_30___Linq_to_SQL
{
    public class Class1
    {
        static void Main()
        {
            //DatasetTest();
            //LinqTest();
            //ModifyCurrency.Insert();
            //ModifyCurrency.Update();
            //ModifyCurrency.Delete();
            //LinqStoredProcedure.OutputManagerEmployees(1);
            //LinqStoredProcedure.OutputManagerEmployeesAndFilter(1);
            LinqJoin.JoinExample();
            LinqJoin.JoinExampleSqlSyntax();
        }

        static void DatasetTest()
        {
            //MyDataSet.Test();
            MyDataSet.EmployeesOnly();
        }

        static void LinqTest()
        {
            //Linq.PersonExample();
            Linq.PersonExampleSqlSyntax();
        }

        static void LinqTest2()
        {
            AdventureWorksDataContext context = new AdventureWorksDataContext();

            var lastNames =
                context.Persons
                    .OrderByDescending(person => person.LastName)
                    .Select(person => person.LastName);

            lastNames =
                from person in context.Persons
                orderby person.LastName descending
                select person.LastName;

            foreach (string lastName in lastNames)
            {
                Console.WriteLine(lastName);
            }
        }
    }
}
