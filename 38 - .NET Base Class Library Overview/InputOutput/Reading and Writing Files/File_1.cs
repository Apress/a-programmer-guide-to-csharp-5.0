// 32 - .NET Frameworks Overview\InputOutput\Reading and Writing Files
// copyright 2000 Eric Gunnerson
using System;
using System.IO;

class Test
{
    public static void Main()
    {
        FileStream f = new FileStream("output.txt", FileMode.Create);
        StreamWriter s = new StreamWriter(f);
        
        s.WriteLine("{0} {1}", "test", 55);
        s.Close();
        f.Close();
    }
}