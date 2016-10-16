// 15 - Conversions\Numeric Types\Conversions and Member Lookup
// copyright 2000 Eric Gunnerson
using System;
class Conv
{
    public static void Process(sbyte value)
    {
        Console.WriteLine("sbyte {0}", value);
    }
    public static void Process(short value)
    {
        Console.WriteLine("short {0}", value);
    }
    public static void Process(int value)
    {
        Console.WriteLine("int {0}", value);
    }
}
class Test
{
    public static void Main()
    {
        int    value1 = 2;
        sbyte    value2 = 1;
        Conv.Process(value1);
        Conv.Process(value2);
    }
}