// 32 - .NET Frameworks Overview\Numeric Formatting\Custom Format Strings\Group Separator
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    Console.WriteLine("{0:##,###}", 2555634323);
    Console.WriteLine("{0:##,000.000}", 14563553.593993);
    Console.WriteLine("{0:#,#.000}", 14563553.593993);
    }
}