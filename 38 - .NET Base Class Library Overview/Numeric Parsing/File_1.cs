// 32 - .NET Frameworks Overview\Numeric Parsing
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
        int value = Int32.Parse("99953");
        double dval = Double.Parse("1.3433E+35");
        Console.WriteLine("{0}", value);
        Console.WriteLine("{0}", dval);
    }
}