using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace LinqToXml
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(CreateXml.CreateXmlExample1DOM());

            //Console.WriteLine(CreateXml.CreateXmlExample1());

            //Console.WriteLine(CreateXml.CreateXmlDocumentExample());

            //Console.WriteLine(CreateXml.CreateXmlExample3());

           // Parsing.TestDescendants();

            //CreateXml.ParseXmlExample();

            //Console.WriteLine(Description.GetExampleXml());

            //Parsing.FindBookNamesExample1();
            //Parsing.FindBookNamesExample2();
            //Parsing.FindBookNamesExample3();

            //Parsing.FindBookPricesExample1();
            //Parsing.FindBookPricesExample2();
            //Parsing.FindBookPricesExample3();

            Parsing.FindBookNamesWithNamespacesExample2();
        }
    }
}
