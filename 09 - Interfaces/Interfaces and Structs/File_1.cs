// 10 - Interfaces\Interfaces and Structs
// copyright 2000 Eric Gunnerson
using System;
struct Number: IComparable
{
    int value;
    
    public Number(int value)
    {
        this.value = value;
    }
    public int CompareTo(object obj2)
    {
        Number num2 = (Number) obj2;
        if (value < num2.value)
        return(-1);
        else if (value > num2.value)
        return(1);
        else
        return(0);
    }
}
class Test
{
    public static void Main()
    {
        Number x = new Number(33);
        Number y = new Number(34);
        
        IComparable Ic = (IComparable) x;
        Console.WriteLine("x compared to y = {0}", Ic.CompareTo(y));
    }
}