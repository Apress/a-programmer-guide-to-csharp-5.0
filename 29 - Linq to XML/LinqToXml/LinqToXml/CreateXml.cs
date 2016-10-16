using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace LinqToXml
{
    class CreateXml
    {
        // Trying to create the following:
        // <books>
        //     <book>
        //         <name>Fox in socks</name>
        //     </book>
        // </books>

        static public string CreateXmlExample1DOM()
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlNode xmlBooksNode = xmlDocument.CreateElement("books");
            xmlDocument.AppendChild(xmlBooksNode);

            XmlNode xmlBookNode = xmlDocument.CreateElement("book");
            xmlBooksNode.AppendChild(xmlBookNode);

            XmlNode xmlNameNode = xmlDocument.CreateElement("name");
            xmlNameNode.InnerText = "Fox in socks";
            xmlBookNode.AppendChild(xmlNameNode);

            XmlNode xmlPriceNode = xmlDocument.CreateElement("price");
            xmlPriceNode.InnerText = "35.99";
            xmlBookNode.AppendChild(xmlPriceNode);

            return xmlDocument.OuterXml;
        }

        static public string CreateXmlExample1()
        {
            XElement element = new XElement("name", "Fox in socks");

            return element.ToString();
        }

        static public string CreateXmlExample2()
        {
XElement element =
    new XElement("books",
        new XElement("book",
            new XElement("name", "Fox in socks"),
            new XElement("price", "35.99")
        )
    );

return element.ToString();
        }

class Book
{
    public Book(string name, Decimal price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; set; }
    public Decimal Price { get; set; }
}

        static List<Book> GetBookList()
        {
            List<Book> books = new List<Book>();
            books.Add(new Book("Fox in socks", 35.99M));
            books.Add(new Book("Rocks in box", 12.99M));
            books.Add(new Book("Lox in crocks", 9.99M));

            return books;
        }

        static public string CreateXmlExample3()
        {
            List<Book> books = GetBookList();

            XElement element =
                new XElement("books",
                        books.Select(x => new XElement("book",
                                            new XElement("name", x.Name),
                                            new XElement("price", x.Price)
                                            )
                                 )
                    );

            return element.ToString();
        }

        static public string CreateXmlExample4()
        {
            List<Book> books = GetBookList();

XElement element =
    new XElement("books",
            books.Select(x => new XElement("book",
                                  new XAttribute("name", x.Name),
                                  new XAttribute("price", x.Price)
                              )
                        )
                );

            return element.ToString();
        }

        static public string CreateXmlExample5()
        {
XNamespace myNamespace = "http://www.example.com";
XElement element = new XElement(myNamespace + "books");

            return element.ToString();
        }

        static public string CreateXmlExampleComment()
        {
XElement element =
    new XElement("books",
        new XElement("book",
            new XComment("name is the short name"),
            new XElement("name", "Fox in socks"),
            new XElement("price", "35.99")
        )
    );

            return element.ToString();
        }

        static public string CreateXmlExampleText()
        {
XElement element =
    new XElement("books",
        new XElement("book",
            new XText("book status"),
            new XElement("name", "Fox in socks"),
            new XElement("price", "35.99")
        )
    );

            return element.ToString();
        }

        static public string CreateXmlDocumentExample()
        {
XDocument document = new XDocument(
    new XProcessingInstruction(
        "xml-stylesheet",
        @"type=""text/xsl"" href=""style.xsl"""),
    new XElement("books"));

document.Declaration = new XDeclaration("1.0", "utf-8", "yes");
document.Save(@"c:\test.xml");

            return document.ToString();
        }

        static public void ParseXmlExample()
        {
XElement element1 = XElement.Load(@"c:\test.xml");
XElement element2 = XElement.Parse("<books><book><name>Fox in Sox</name></book></books>");

Console.WriteLine(element1);
Console.WriteLine(element2);



        }
    }
}


