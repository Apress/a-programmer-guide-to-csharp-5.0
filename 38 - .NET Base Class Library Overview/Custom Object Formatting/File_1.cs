// 32 - .NET Frameworks Overview\Custom Object Formatting
// copyright 2000 Eric Gunnerson
using System;
class Employee: IFormattable
{
    public Employee(int id, string firstName, string lastName)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
    }
    public string ToString (string format, IFormatProvider fp) 
    {
        if ((format != null) && (format.Equals("F")))
        return(String.Format("{0}: {1}, {2}", 
        id, lastName, firstName));
        else
        return(id.ToString(format, fp));
    }
    int    id;
    string    firstName;
    string    lastName;
}
class Test
{
    public static void Main()
    {
        Employee fred = new Employee(123, "Fred", "Morthwaite");
        Console.WriteLine("No format: {0}", fred);
    Console.WriteLine("Full format: {0:F}", fred);
    }
}