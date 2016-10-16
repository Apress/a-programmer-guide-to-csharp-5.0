// 32 - .NET Frameworks Overview\Numeric Formatting\Standard Format Strings\Fixed-Point
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    Console.WriteLine("{0:F}", 33345.8977);
    Console.WriteLine("{0:F0}", 33345.8977);
    Console.WriteLine("{0:F5}", 33345.8977);
    }
}