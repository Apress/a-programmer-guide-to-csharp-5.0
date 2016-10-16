// 15 - Conversions\Numeric Types\Checked Conversions
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
        checked
        {
            uint value1 = 312;
            byte value2 = (byte) value1;
            Console.WriteLine("Value: {0}", value2);
        }
    }
}