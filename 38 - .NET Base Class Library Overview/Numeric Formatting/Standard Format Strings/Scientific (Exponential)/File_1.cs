// 32 - .NET Frameworks Overview\Numeric Formatting\Standard Format Strings\Scientific (Exponential)
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    Console.WriteLine("{0:E}", 33345.8977);
    Console.WriteLine("{0:E10}", 33345.8977);
    Console.WriteLine("{0:e4}", 33345.8977);
    }
}