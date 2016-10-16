using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_6___Member_accessibility_and_overloading
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();
            logger.LogMessage("Started", "Main");

            logger.LogMessage(message: "Started", component: "Main");
        }
    }
}
