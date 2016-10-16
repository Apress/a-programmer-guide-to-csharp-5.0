using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Xml.XPath;

namespace LinqToXml
{
    public class Parsing
    {
        public static XElement GetExampleXml()
        {
            XElement element =
                new XElement("books",
                    new XElement("book",
                        new XElement("name", "Fox in Sox"),
                        new XElement("price", "12.95"),
                        new XElement("editions",
                            new XElement("edition",
                                new XElement("number", "1"),
                                new XElement("year", "1956"),
                                new XElement("price", "1.49")
                            ),
                            new XElement("edition",
                                new XElement("number", "2"),
                                new XElement("year", "1973"),
                                new XElement("price", "5.59")
                            )
                        ),
                        new XElement("pages", "55")
                    ),
                    new XElement("book",
                        new XElement("name", "Fox in Crocs"),
                        new XElement("price", "9.95")
                    )
                );

            return element;
        }

        public static XElement GetExampleXmlWithNamespaces()
        {
            XNamespace booksNamespace = "http://examplelibrary.com";
            XNamespace priceNamespace = "http://pricelibrary.com";

            XElement element =
                new XElement(booksNamespace + "books",
                    new XElement(booksNamespace + "book",
                        new XElement(booksNamespace + "name", "Fox in Sox"),
                        new XElement(priceNamespace + "price", "12.95"),
                        new XElement(booksNamespace + "pages", "55")
                    ),
                    new XElement(booksNamespace + "book",
                        new XElement(booksNamespace + "name", "Fox in Crocs"),
                        new XElement(priceNamespace + "price", "9.95")
                    )
                );

            return element;
        }


        public static XElement GetExampleXmlWithNamespaceShortcuts()
        {
XNamespace booksNamespace = "http://examplelibrary.com";
XNamespace priceNamespace = "http://pricelibrary.com";

XElement element =
    new XElement(booksNamespace + "books",
        new XAttribute(XNamespace.Xmlns + "bk", booksNamespace.NamespaceName),
        new XAttribute(XNamespace.Xmlns + "pr", priceNamespace.NamespaceName),
        new XElement(booksNamespace + "book",
            new XElement(booksNamespace + "name", "Fox in Sox"),
            new XElement(priceNamespace + "price", "12.95"),
            new XElement(booksNamespace + "pages", "55")
        )
    );

            return element;
        }

        public static void TestDescendants()
        {
            XElement booksElement = GetExampleXml();

            XElement editions = booksElement.Element("book").Element("editions");

            foreach (XElement x in editions.Descendants())
            {
                Console.WriteLine(x.Name);
            }
        }

        public static void FindBookNamesExample1()
        {
            XElement booksElement = GetExampleXml();

            var books = booksElement.Elements("book");

            foreach (var book in books)
            {
                Console.WriteLine(book.Element("name").Value);
            }
        }

        public static void FindBookNamesExample2()
        {
            XElement booksElement = GetExampleXml();

            var bookNames = booksElement.Elements("book")
                .SelectMany(book => book.Elements("name"))
                .Select(name => name.Value);

            foreach (var bookName in bookNames)
            {
                Console.WriteLine(bookName);
            }
        }

        public static void FindBookNamesExample3()
        {
            XElement booksElement = GetExampleXml();

            var bookNames = booksElement.Descendants("name").Select(name => name.Value);

            foreach (var bookName in bookNames)
            {
                Console.WriteLine(bookName);
            }
        }

        public static void FindBookPricesExample1()
        {
            XElement booksElement = GetExampleXml();

            var bookPrices = booksElement.Descendants("price").Select(name => name.Value);

            foreach (var bookPrice in bookPrices)
            {
                Console.WriteLine(bookPrice);
            }
        }

        public static void FindBookPricesExample2()
        {
            XElement booksElement = GetExampleXml();

            var bookPrices = booksElement.XPathSelectElements("book/price").Select(name => name.Value);

            foreach (var bookPrice in bookPrices)
            {
                Console.WriteLine(bookPrice);
            }
        }

        public static void FindBookPricesExample3()
        {
            XElement book = new XElement("book",
                                new XElement("price",
                                    new XAttribute("full", "15.99"),
                                    new XAttribute("wholesale", "7.99")
                                )
                            );

            Console.WriteLine(book);

            var wholesale2 = ((IEnumerable<object>)book
                    .XPathEvaluate("price/@wholesale"))
                    .Cast<XAttribute>()
                    .Select(att => att.Value)
                    .First();

            string wholesale = book.XPathSelectAttribute("price/@wholesale");

            Console.WriteLine(wholesale2);
        }


        public static void FindBookNamesWithNamespacesExample1()
        {
            XElement booksElement = GetExampleXmlWithNamespaces();
            Console.WriteLine(booksElement);

            XNamespace booksNamespace = "http://examplelibrary.com";
            XNamespace priceNamespace = "http://pricelibrary.com";
            var books = booksElement.Elements(booksNamespace + "book");

            foreach (var book in books)
            {
                Console.WriteLine(book.Element(booksNamespace + "name").Value);
                Console.WriteLine(book.Element(priceNamespace + "price").Value);
            }
        }


        public static void FindBookNamesWithNamespacesExample2()
        {
            XElement booksElement = GetExampleXmlWithNamespaceShortcuts();
            Console.WriteLine(booksElement);

XNamespace booksNamespace = "http://examplelibrary.com";
XNamespace priceNamespace = "http://pricelibrary.com";
var books = booksElement.Elements(booksNamespace + "book");

foreach (var book in books)
{
    Console.WriteLine(book.Element(booksNamespace + "name").Value);
    Console.WriteLine(book.Element(priceNamespace + "price").Value);

string temp = book.Element(priceNamespace + "price").Value;
Decimal price = Decimal.Parse(temp);
Console.WriteLine(price);

price = (Decimal)book.Element(priceNamespace + "price");
Console.WriteLine(price);
}
        }
    }
}

//GetExampleXmlWithNamespaceShortcuts

public static class XPathHelper
{
    public static IEnumerable<string> XPathSelectAttributes(
        this XElement element,
        string xpathExpression)
    {
        return ((IEnumerable<object>)element.XPathEvaluate(xpathExpression))
            .Cast<XAttribute>()
            .Select(att => att.Value);
    }

    public static string XPathSelectAttribute(
        this XElement element,
        string xpathExpression)
    {
        return XPathSelectAttributes(element, xpathExpression).First();
    }

}
