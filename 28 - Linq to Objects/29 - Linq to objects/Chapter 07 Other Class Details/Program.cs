using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_07_Other_Class_Details
{
    class Program
    {

        static void Main(string[] args)
        {
            Employee emp = new Employee();
            emp.Name = "Fred";
            emp.Age = 35;
            emp.Salary = 13233m;

            PartialExample();
        }

        static void PartialExample()
        {
Saluter saluter = new Saluter(21);
saluter.Ready();
saluter.Aim();
saluter.Fire();
        }
    }


}
