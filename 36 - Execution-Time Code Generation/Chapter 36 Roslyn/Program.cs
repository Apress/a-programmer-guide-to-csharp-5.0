using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.CSharp;
using System.Reflection;
using System.IO;

namespace Roslyn
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double> cuber = CuberGenerator.GetCuber(7);
            Console.WriteLine(cuber());
        }
    }
}
