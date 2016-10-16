// 20 - Enumerations\The System.Enum Type
// copyright 2000 Eric Gunnerson
using System;

enum Color
{
    red,
    green,
    yellow
}

public class Test
{
    public static void Main()
    {
        Color c = Color.red;
        
        // enum values and names
        foreach (int i in Enum.GetValues(c.GetType()))
        {
            Console.WriteLine("Value: {0} ({1})", i, Enum.GetName(c.GetType(), i));
        }
        
        // or just the names
        foreach (string s in Enum.GetNames(c.GetType()))
        {
            Console.WriteLine("Name: {0}", s);
        }
        
        // enum value from a string, ignore case
        c = (Color) Enum.Parse(typeof(Color), "Red", true);
        Console.WriteLine("string value is: {0}", c);
        
        // see if a specific value is a defined enum member
        bool defined = Enum.IsDefined(typeof(Color), 5);
    Console.WriteLine("5 is a defined value for Color: {0}", defined);    }
}