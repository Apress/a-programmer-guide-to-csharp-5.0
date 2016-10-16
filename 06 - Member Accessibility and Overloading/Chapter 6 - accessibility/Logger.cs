using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_6___accessibility
{
public class Logger
{
    public static void LogMessage(string message, bool includeDateAndTime)
    {
        if (includeDateAndTime)
        {
            Console.WriteLine(DateTime.Now);
        }
        Console.WriteLine(message);
    }
}
}
