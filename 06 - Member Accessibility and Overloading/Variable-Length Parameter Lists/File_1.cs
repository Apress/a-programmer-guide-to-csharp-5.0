// 07 - Member Accessibility and Overloading\Variable-Length Parameter Lists
// copyright 2000 Eric Gunnerson
using System;
class Port
{
    // version with a single object parameter
    public void Write(string label, object arg)
    {
        WriteString(label);
        WriteString(arg.ToString());
    }
    // version with an array of object parameters
    public void Write(string label, params object[] args)
    {
        WriteString(label);
        foreach (object o in args)
        {
            WriteString(o.ToString());
        }    
    }
    void WriteString(string str)
    {
        // writes string to the port here
        Console.WriteLine("Port debug: {0}", str);
    }
}

class Test
{
    public static void Main()
    {
        Port    port = new Port();
        port.Write("Single Test", "Port ok");
        port.Write("Port Test: ", "a", "b", 12, 14.2);
        object[] arr = new object[4];
        arr[0] = "The";
        arr[1] = "answer";
        arr[2] = "is";
        arr[3] = 42;
        port.Write("What is the answer?", arr);
    }
}