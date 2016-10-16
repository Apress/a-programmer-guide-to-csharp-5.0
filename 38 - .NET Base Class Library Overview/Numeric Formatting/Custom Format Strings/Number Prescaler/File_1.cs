// 32 - .NET Frameworks Overview\Numeric Formatting\Custom Format Strings\Number Prescaler
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    Console.WriteLine("{0:000,.##}", 158847);
    Console.WriteLine("{0:000,,,.###}", 1593833);
    }
}