using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_26___operator_overloading
{
struct RomanNumeral
{
    public RomanNumeral(int value)
    {
        m_value = value;
    }
    public override string ToString()
    {
        return m_value.ToString();
    }
    public static RomanNumeral operator -(RomanNumeral roman)
    {
        return new RomanNumeral(-roman.m_value);
    }
    public static RomanNumeral operator +(
    RomanNumeral roman1,
    RomanNumeral roman2)
    {
        return new RomanNumeral(roman1.m_value + roman2.m_value);
    }

    public static RomanNumeral operator ++(RomanNumeral roman)
    {
        return new RomanNumeral(roman.m_value + 1);
    }
    int m_value;
}
class Testy
{
    public static void Mainly()
    {
        RomanNumeral roman1 = new RomanNumeral(12);
        RomanNumeral roman2 = new RomanNumeral(125);

        Console.WriteLine("Increment: {0}", roman1++);
        Console.WriteLine("Addition: {0}", roman1 + roman2);
    }
}

}
