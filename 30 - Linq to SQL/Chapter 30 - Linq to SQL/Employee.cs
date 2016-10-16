using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_30___Linq_to_SQL
{
#if OldSchool
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime DateOfBirth { get; set; }

    public Person(IDataReader reader)
    {
        int i = 0;
        Name =          (string)reader[i++];
        Age =           (int)reader[i++];
        DateOfBirth =   (DateTime)reader[i++];
    }
}
#endif
class EmployeeFetcher
{
#if fred
    List<Employee> GetEmployees(string query)
    {
        List<Employee> results = new List<Employee>();

        SqlDataReader reader = Database.ExecuteReader(query);

        while (reader.Read())
        {
            Employee employee = new Employee(reader);
            results.Add(employee);
        }

        return results;
    }
#endif
}
}
