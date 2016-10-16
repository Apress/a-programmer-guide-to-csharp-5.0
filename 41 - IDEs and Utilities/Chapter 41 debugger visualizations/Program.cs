using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        string commaSeparatedSample = "a,b,c,d,efg";
        CommaSeparatedValues commaSeparatedValues = new CommaSeparatedValues(commaSeparatedSample);
            Console.WriteLine(commaSeparatedValues);

        List<string> list = new List<string>();
        list.Add("Fred");
    }
}
