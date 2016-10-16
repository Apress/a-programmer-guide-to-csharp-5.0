// 15 - Conversions\Numeric Types\Checked Conversions
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
        uint value1 = 312;
        byte value2;
        
        value2 = unchecked((byte) value1);    // never checked
        value2 = (byte) value1;            // checked if /checked
        value2 = checked((byte) value1);        // always checked
    }
}