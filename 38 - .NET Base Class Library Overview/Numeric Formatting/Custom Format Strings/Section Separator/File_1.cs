// 32 - .NET Frameworks Overview\Numeric Formatting\Custom Format Strings\Section Separator
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    Console.WriteLine("{0:###.00;0;(###.00)}", -456.55);
    Console.WriteLine("{0:###.00;0;(###.00)}", 0);
    Console.WriteLine("{0:###.00;0;(###.00)}", 456.55);
    }
}