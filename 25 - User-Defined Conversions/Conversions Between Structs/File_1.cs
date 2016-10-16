// 24 - User-Defined Conversions\Conversions Between Structs
// copyright 2000 Eric Gunnerson
using System;
using System.Text;
struct RomanNumeral
{
    public RomanNumeral(short value) 
    {
        if (value > 5000)
        throw(new ArgumentOutOfRangeException());
        
        this.value = value;
    }
    public static explicit operator RomanNumeral(
    short value) 
    {
        RomanNumeral    retval;
        retval = new RomanNumeral(value);
        return(retval);
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
    
    private short value;
}
struct BinaryNumeral
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
class Test
{
    public static void Main()
    {
        RomanNumeral    roman = new RomanNumeral(12);
        BinaryNumeral    binary;
        binary = (BinaryNumeral)(int)roman;
    }
}