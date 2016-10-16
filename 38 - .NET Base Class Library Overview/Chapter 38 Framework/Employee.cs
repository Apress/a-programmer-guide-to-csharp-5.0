using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chapter_38_Framework
{
[XmlType(TypeName = "Employee2005")]
public class Employee
{
    [XmlElement("FullName")]
    public string Name { get; set; }

    [XmlAttribute("Salary")]
    public Decimal Salary { get; set; }

    public DateTime DateOfBirth { get; set; }

    [XmlIgnore]
    public int Weight { get; set; }
}
}
