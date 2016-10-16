// 14 - Operators and Expressions\Checked and Unchecked Expressions
// copyright 2000 Eric Gunnerson
using System;

class Test
{
    public static void Main()
    {
        unchecked
        {
            byte a = 55;
            byte b = 210;
            byte c = (byte) (a + b);
        }
    }
}