using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Chapter_31_Other_Language_Details
{
class Employee: IXmlSerializable
{
    public string Name { get; set; }
    public Decimal Age { get; set; }

    public Employee(String name, Decimal age)
    {
        Name = name;
        Age = age;
    }

    #region IXmlSerializable

    // IXmlSerializable methods go here...
    #endregion
}
}
