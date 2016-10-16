// 32 - .NET Frameworks Overview\Numeric Formatting\Custom Format Strings\Digit or Space Placeholder
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    Console.WriteLine("{0:#####}", 255);
    Console.WriteLine("{0:#####}", 1456);
    Console.WriteLine("{0:###}", 32767);
    }
}