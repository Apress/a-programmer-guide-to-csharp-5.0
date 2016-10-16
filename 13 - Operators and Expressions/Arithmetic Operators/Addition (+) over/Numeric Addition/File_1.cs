// 14 - Operators and Expressions\Arithmetic Operators\Addition (+) over\Numeric Addition
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
        byte val1 = 200;
        byte val2 = 201;
        byte sum = (byte) (val1 + val2);   // no exception
        checked
        {
            byte sum2 = (byte) (val1 + val2);      // exception
        }
    }
}