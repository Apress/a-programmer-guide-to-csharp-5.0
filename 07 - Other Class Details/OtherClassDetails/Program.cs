using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtherLanguageDetails
{
    class Program
    {
        static void Main(string[] args)
        {
            Original();
            Method();
            ClassHelperMethod();
            ExtensionMethod();
        }

        static void Original()
        {
string test = "#Name#,#Date#,#Age#,#Salary#";

string[] fieldArray = test.Split(',');

List<string> fields = new List<string>();

foreach (string field in fieldArray)
{
    fields.Add(field.Replace("#", ""));
}

            foreach (string field in fields)
            {
                Console.WriteLine(field);
            }
        }

        static void Method()
        {
string test = "#Name#,#Date#,#Age#,#Salary#";

List<string> fields = ExtractFields(test);

foreach (string field in fields)
{
    Console.WriteLine(field);
}
        }

        static void ClassHelperMethod()
        {
            string test = "#Name#,#Date#,#Age#,#Salary#";

List<string> fields = StringHelper.ExtractFields(test);

            foreach (string field in fields)
            {
                Console.WriteLine(field);
            }
        }

        static void ExtensionMethod()
        {
            string test = "#Name#,#Date#,#Age#,#Salary#";

            List<string> fields = test.ExtractFields();

            foreach (string field in fields)
            {
                Console.WriteLine(field);
            }
        }

static List<string> ExtractFields(string fieldString)
{
    string[] fieldArray = fieldString.Split(',');

    List<string> fields = new List<string>();

    foreach (string field in fieldArray)
    {
        fields.Add(field.Replace("#", ""));
    }

    return fields;
}
    }

    public static class StringHelper
    {
        public static List<string> ExtractFields(string fieldString)
        {
            string[] fieldArray = fieldString.Split(',');

            List<string> fields = new List<string>();

            foreach (string field in fieldArray)
            {
                fields.Add(field.Replace("#", ""));
            }

            return fields;
        }
    }

    public static class StringExtensions
    {
        public static List<string> ExtractFields(this string fieldString)
        {
            string[] fieldArray = fieldString.Split(',');

            List<string> fields = new List<string>();

            foreach (string field in fieldArray)
            {
                fields.Add(field.Replace("#", ""));
            }

            return fields;
        }
    }
}
