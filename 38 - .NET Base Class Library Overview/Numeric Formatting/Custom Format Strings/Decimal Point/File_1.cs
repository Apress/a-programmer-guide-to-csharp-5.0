// 32 - .NET Frameworks Overview\Numeric Formatting\Custom Format Strings\Decimal Point
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    Console.WriteLine("{0:#####.000}", 75928.3);
    Console.WriteLine("{0:##.000}", 1456.456456);
    }
}