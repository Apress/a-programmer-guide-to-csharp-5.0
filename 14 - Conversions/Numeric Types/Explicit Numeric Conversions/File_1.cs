// 15 - Conversions\Numeric Types\Explicit Numeric Conversions
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
        uint value1 = 312;
        byte value2 = (byte) value1;
        Console.WriteLine("Value2: {0}", value2);
    }
}