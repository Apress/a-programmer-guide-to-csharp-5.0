// 32 - .NET Frameworks Overview\Numeric Formatting\Custom Format Strings\Digit or Zero Placeholder
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    Console.WriteLine("{0:000}", 55);
    Console.WriteLine("{0:000}", 1456);
    }
}