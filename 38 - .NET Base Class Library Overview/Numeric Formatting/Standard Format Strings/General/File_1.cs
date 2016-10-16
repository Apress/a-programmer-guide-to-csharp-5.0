// 32 - .NET Frameworks Overview\Numeric Formatting\Standard Format Strings\General
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    Console.WriteLine("{0:G}", 33345.8977);
    Console.WriteLine("{0:G7}", 33345.8977);
    Console.WriteLine("{0:G4}", 33345.8977);
    }
}