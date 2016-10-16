using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log_Driver_Tester
{

class Test
{
    public static void Main()
    {
        LogDriver logDriver = new LogDriver();

        logDriver.AddLogger(new LogConsole());

        logDriver.Log("Log start: " + DateTime.Now.ToString());

        for (int i = 0; i < 5; i++)
        {
            logDriver.Log("Operation: " + i.ToString());
        }

        logDriver.Log("Log end: " + DateTime.Now.ToString());
    }
}
}
