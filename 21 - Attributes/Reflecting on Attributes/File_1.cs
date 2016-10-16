// 21 - Attributes\Reflecting on Attributes
// copyright 2000 Eric Gunnerson
using System;
using System.Reflection;
[AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
public class CodeReviewAttribute: System.Attribute
{
    public CodeReviewAttribute(string reviewer, string date)
    {
        this.reviewer = reviewer;
        this.date = date;
    }
    public string Comment
    {
        get
        {
            return(comment);
        }
        set
        {
            comment = value;
        }
    }
    public string Date
    {
        get
        {
            return(date);
        }
    }
    public string Reviewer
    {
        get
        {
            return(reviewer);
        }
    }
    string reviewer;
    string date;
    string comment;
}
[CodeReview("Eric", "01-12-2000", Comment="Bitchin' Code")]
[CodeReview("Gurn", "01-01-2000", Comment="Revisit this section")]
class Complex
{
}

class Test
{
    public static void Main()
    {
        Type type = typeof(Complex);
        foreach (CodeReviewAttribute att in
        type.GetCustomAttributes(typeof(CodeReviewAttribute), false))
        {
            Console.WriteLine("Reviewer: {0}", att.Reviewer);
            Console.WriteLine("Date: {0}", att.Date);
            Console.WriteLine("Comment: {0}", att.Comment);
        }
    }
}