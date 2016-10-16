using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_38_Framework
{
    public class Class1
    {
        public static void Main()
        {
            //RoundTrip.Mainly();
            //ReadTextFile.Mainly();
            //DirectoryWalkerTest.Mainly();
            //SerializationTest.Mainly();
            //CustomSerializationTest.Mainly();
            //WebPageTest.Mainly();
            //EnvironmentTest.Mainly();
            XmlSerializationTest.SaveEmployee();
            XmlSerializationTest.LoadEmployee();
        }
    }
}
