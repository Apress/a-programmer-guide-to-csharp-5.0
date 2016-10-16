using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chapter_38_Framework
{
    class XmlSerializationTest
    {
public static void SaveEmployee()
{
    Employee employee = new Employee();
    employee.Name = "Peter";
    employee.Salary = 15123M;
    employee.DateOfBirth = DateTime.Parse("12/31/1994");

    XmlSerializer serializer = new XmlSerializer(typeof(Employee));

    using (Stream writeStream = File.Open("Employee.xml", FileMode.Create))
    {
        serializer.Serialize(writeStream, employee);
    }
}

public static Employee LoadEmployee()
{
    XmlSerializer serializer = new XmlSerializer(typeof(Employee));

    using (Stream readStream = File.OpenRead("Employee.xml"))
    {
        return (Employee) serializer.Deserialize(readStream);
    }
}
    }
}
