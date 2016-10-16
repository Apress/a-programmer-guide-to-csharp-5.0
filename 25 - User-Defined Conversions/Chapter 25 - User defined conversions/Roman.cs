using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_25___User_defined_conversions
{
using System;
using System.Text;
struct RomanNumeral
{
    public RomanNumeral(short value)
    {
        if (value > 5000)
        {
            throw (new ArgumentOutOfRangeException());
        }
        m_value = value;
    }
    public static explicit operator RomanNumeral(short value)
    {
        RomanNumeral retval;
        retval = new RomanNumeral(value);
        return (retval);
    }

    public static implicit operator short(RomanNumeral roman)
    {
        return (roman.m_value);
    }

    static string NumberString(ref int value, int magnitude, char letter)
    {
        StringBuilder numberString = new StringBuilder();

        while (value >= magnitude)
        {
            value -= magnitude;
            numberString.Append(letter);
        }
        return (numberString.ToString());
    }

    public static implicit operator string(RomanNumeral roman)
    {
        int temp = roman.m_value;

        StringBuilder retval = new StringBuilder();

        retval.Append(RomanNumeral.NumberString(ref temp, 1000, 'M'));
        retval.Append(RomanNumeral.NumberString(ref temp, 500, 'D'));
        retval.Append(RomanNumeral.NumberString(ref temp, 100, 'C'));
        retval.Append(RomanNumeral.NumberString(ref temp, 50, 'L'));
        retval.Append(RomanNumeral.NumberString(ref temp, 10, 'X'));
        retval.Append(RomanNumeral.NumberString(ref temp, 5, 'V'));
        retval.Append(RomanNumeral.NumberString(ref temp, 1, 'I'));

        return retval.ToString();
    }

    public override string ToString()
    {
        return (string) this;
    }

    private short m_value;
}
class Test2
{
    public static void Mainly()
    {
        short s = 12;
        RomanNumeral numeral = new RomanNumeral(s);

        s = 165;
        numeral = (RomanNumeral)s;

        Console.WriteLine("Roman as int: {0}", (int)numeral);
        Console.WriteLine("Roman as string: {0}", (string)numeral);

        short s2 = numeral; 

        Console.WriteLine("{0}", numeral);
    }
}

}
