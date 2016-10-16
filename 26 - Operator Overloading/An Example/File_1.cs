// 25 - Operator Overloading\An Example
// copyright 2000 Eric Gunnerson
using System;
struct RomanNumeral
{
    public RomanNumeral(int value)
    {
        this.value = value;
    }
    public override string ToString()
    {
        return(value.ToString());
    }
    public static RomanNumeral operator -(RomanNumeral roman)
    {
        return(new RomanNumeral(-roman.value));
    }
    public static RomanNumeral operator +(
    RomanNumeral    roman1,
    RomanNumeral    roman2)
    {
        return(new RomanNumeral(
        roman1.value + roman2.value));
    }
    
    public static RomanNumeral operator ++(
    RomanNumeral    roman)
    {
        return(new RomanNumeral(roman.value + 1));
    }
    int value;
}
class Test
{
    public static void Main()
    {
        RomanNumeral    roman1 = new RomanNumeral(12);
        RomanNumeral    roman2 = new RomanNumeral(125);
        
        Console.WriteLine("Increment: {0}", roman1++);
        Console.WriteLine("Addition: {0}", roman1 + roman2);
    }
}