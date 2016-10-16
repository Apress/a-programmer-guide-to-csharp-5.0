using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace Chapter_24___Dynamic_types
{
    delegate int GetValueDelegate();

    class Program
    {
        static void Main(string[] args)
        {
            //SimpleExample.Run();

            //UnknownMethodExample.Run();

            GenericArithmetic.Test();
        }
    }
}
