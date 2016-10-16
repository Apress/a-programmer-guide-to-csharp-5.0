using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_22___Delegates
{
    class Customer
    {
        public Customer(string name, string company)
        {
            Name = name;
            Company = company;
        }

        public string Name { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public int YearOfBirth { get; set; }
    }

}
