// 32 - .NET Frameworks Overview\Numeric Formatting\Custom Format Strings\Exponential Notation
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    Console.WriteLine("{0:###.000E-00}", 3.1415533E+04);
    Console.WriteLine("{0:#.0000000E+000}", 2.553939939E+101);
    }
}