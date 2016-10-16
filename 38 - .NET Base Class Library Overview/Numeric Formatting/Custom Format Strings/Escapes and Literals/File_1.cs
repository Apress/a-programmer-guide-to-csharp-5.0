// 32 - .NET Frameworks Overview\Numeric Formatting\Custom Format Strings\Escapes and Literals
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    Console.WriteLine("{0:###\\#}", 255);
    Console.WriteLine(@"{0:###\#}", 255);
    Console.WriteLine("{0:###'#0%;'}", 1456);
    }
}