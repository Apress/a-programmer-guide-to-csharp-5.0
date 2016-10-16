// 24 - User-Defined Conversions\Classes and Pre and Post Conversions
// copyright 2000 Eric Gunnerson
using System;
using System.Text;
class RomanNumeral
{
    public RomanNumeral(short value) 
    {
        if (value > 5000)
        throw(new ArgumentOutOfRangeException());
        
        this.value = value;
    }
    public static implicit operator short(
    RomanNumeral roman)
    {
        return(roman.value);
    }
    
    static string NumberString(
    ref int value, int magnitude, char letter)
    {
        StringBuilder    numberString = new StringBuilder();
        
        while (value >= magnitude)
        {
            value -= magnitude;
            numberString.Append(letter);
        }
        return(numberString.ToString());
    }
    
    public static implicit operator string(
    RomanNumeral roman)
    {
        int        temp = roman.value;
        
        StringBuilder retval = new StringBuilder();
        
        retval.Append(RomanNumeral.NumberString(ref temp, 1000, 'M'));
        retval.Append(RomanNumeral.NumberString(ref temp, 500, 'D'));
        retval.Append(RomanNumeral.NumberString(ref temp, 100, 'C'));
        retval.Append(RomanNumeral.NumberString(ref temp, 50, 'L'));
        retval.Append(RomanNumeral.NumberString(ref temp, 10, 'X'));
        retval.Append(RomanNumeral.NumberString(ref temp, 5, 'V'));
        retval.Append(RomanNumeral.NumberString(ref temp, 1, 'I'));
        
        return(retval.ToString());
    }
    public static implicit operator BinaryNumeral(RomanNumeral roman)
    {
        return(new BinaryNumeral((short) roman));
    }
    
    public static explicit operator RomanNumeral(
    BinaryNumeral binary)
    {
        int        val = binary;
        if (val >= 1000)
        return((RomanNumeral) 
        new RomanNumeralAlternate((short) val));
        else
        return(new RomanNumeral((short) val));
    }
    
    private short value;
}
class BinaryNumeral
{
    public BinaryNumeral(int value) 
    {
        this.value = value;
    }
    public static implicit operator BinaryNumeral(
    int value) 
    {
        BinaryNumeral    retval = new BinaryNumeral(value);
        return(retval);
    }
    
    public static implicit operator int(
    BinaryNumeral binary)
    {
        return(binary.value);
    }
    
    public static implicit operator string(
    BinaryNumeral binary)
    {
        StringBuilder    retval = new StringBuilder();
        
        return(retval.ToString());
    }
    
    private int value;
}
class RomanNumeralAlternate : RomanNumeral
{
    public RomanNumeralAlternate(short value) : base(value)
    {
    }
    
    public static implicit operator string(
    RomanNumeralAlternate roman)
    {
        return("NYI");
    }
}
class Test
{
    public static void Main()
    {
        // implicit conversion section
        RomanNumeralAlternate    roman;
        roman = new RomanNumeralAlternate(55);
        BinaryNumeral binary = roman;
        
        // explicit conversion section
        BinaryNumeral binary2 = new BinaryNumeral(1500);
        RomanNumeralAlternate roman2;
        
        roman2 = (RomanNumeralAlternate) binary2;
    }
}