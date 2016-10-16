using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Logger
{
    public void LogMessage(string message, string component)
    {
        Console.WriteLine("{0} {1}", component, message);
    }

    public void LogMessage(string message)
    {
        LogMessage(message, "Main");
    }
}

public class Logger2
{
    public void LogMessage(string message, string component = "Main")
    {
        Console.WriteLine("{0} {1}", component, message);
    }

}


