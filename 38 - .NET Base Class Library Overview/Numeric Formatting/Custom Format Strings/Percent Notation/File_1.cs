// 32 - .NET Frameworks Overview\Numeric Formatting\Custom Format Strings\Percent Notation
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    Console.WriteLine("{0:##.000%}", 0.89144);
    Console.WriteLine("{0:00%}", 0.01285);
    }
}