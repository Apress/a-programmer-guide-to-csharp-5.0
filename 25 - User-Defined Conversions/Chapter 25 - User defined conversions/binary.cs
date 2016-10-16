using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_25___User_defined_conversions
{
struct BinaryNumeral
{
    public BinaryNumeral(int value)
    {
        m_value = value;
    }
    public static implicit operator BinaryNumeral(int value)
    {
        return new BinaryNumeral(value);
    }
    public static implicit operator int(BinaryNumeral binary)
    {
        return binary.m_value;
    }

    public static implicit operator string(
    BinaryNumeral binary)
    {
        StringBuilder retval = new StringBuilder();

        return (retval.ToString());
    }

    private int m_value;
}
class Test 
{
    public static void Mainly()
    {
        RomanNumeral roman = new RomanNumeral(12);
        BinaryNumeral binary;
        binary = (BinaryNumeral)(int)roman;
    }
}

}
