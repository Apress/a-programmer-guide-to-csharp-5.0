// 15 - Conversions\Numeric Types\Conversions and Member Lookup
// copyright 2000 Eric Gunnerson
using System;
class Conv
{
    public static void Process(short value)
    {
        Console.WriteLine("short {0}", value);
    }
    public static void Process(ushort value)
    {
        Console.WriteLine("ushort {0}", value);
    }
}
class Test
{
    public static void Main()
    {
        byte    value = 3;
        Conv.Process(value);
    }
}