using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetworkEmployee = MyCompany.Computer.Network.Model.Classes.Employee;
using HREmployee = MyCompany.HumanResources.Application.DataModel.Employee;

namespace Chapter_10_Versioning
{
    class Program
    {
        static void Main(string[] args)
        {

        }

public MyCompany.Computer.Network.Model.Classes.Employee CopyEmployeeData2(MyCompany.HumanResources.Application.DataModel.Employee hrEmployee)
{
    MyCompany.Computer.Network.Model.Classes.Employee networkEmployee = new MyCompany.Computer.Network.Model.Classes.Employee();
    networkEmployee.Name = hrEmployee.Name;

    return networkEmployee;
}

public NetworkEmployee CopyEmployeeData(HREmployee hrEmployee)
{
    NetworkEmployee networkEmployee = new NetworkEmployee();
    networkEmployee.Name = hrEmployee.Name;

    return networkEmployee;
}

    }
}
