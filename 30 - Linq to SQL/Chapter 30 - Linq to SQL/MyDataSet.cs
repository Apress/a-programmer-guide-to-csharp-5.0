using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_30___Linq_to_SQL
{
    class MyDataSet
    {
        public static void Test()
        {
PersonDataSetTableAdapters.PersonTableAdapter personTableAdapter =
    new PersonDataSetTableAdapters.PersonTableAdapter();

PersonDataSet.PersonDataTable personDataTable = personTableAdapter.GetData();

foreach (PersonDataSet.PersonRow personRow in personDataTable.Rows)
{
    Console.WriteLine(personRow.PersonType);


}
        }

        public static void EmployeesOnly()
        {
PersonDataSetTableAdapters.PersonTableAdapter personTableAdapter =
    new PersonDataSetTableAdapters.PersonTableAdapter();

PersonDataSet.PersonDataTable personDataTable = personTableAdapter.GetData();

List<PersonDataSet.PersonRow> employees = new List<PersonDataSet.PersonRow>();
foreach (PersonDataSet.PersonRow personRow in personDataTable.Rows)
{
    if (personRow.PersonType == "EM")
    {
        employees.Add(personRow);
    }
}

foreach (PersonDataSet.PersonRow personRow in employees)
{
    Console.WriteLine(personRow.LastName);
}
        }

        public static void EmployeesOnlyLinqish()
        {
PersonDataSetTableAdapters.PersonTableAdapter personTableAdapter =
    new PersonDataSetTableAdapters.PersonTableAdapter();

PersonDataSet.PersonDataTable personDataTable = personTableAdapter.GetData();

List<PersonDataSet.PersonRow> persons = new List<PersonDataSet.PersonRow>();
foreach (PersonDataSet.PersonRow personRow in personDataTable.Rows)
{
    persons.Add(personRow);
}

var employees = persons.Where(person => person.PersonType == "EM");
        }

    }
}
