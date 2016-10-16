using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_38_Framework
{
    class ReadTextFile
    {
        public static void Mainly()
        {
            using (StreamWriter writer = File.CreateText("output.txt"))
            {
                writer.WriteLine("{0} {1}", "test", 55);
            }

using (StreamReader reader = File.OpenText("output.txt"))
{
    string line = reader.ReadLine();
    Console.WriteLine(line);
}
        }
    }
}
