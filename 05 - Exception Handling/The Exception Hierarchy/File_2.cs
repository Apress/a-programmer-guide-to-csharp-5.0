// 04 - Exception Handling\The Exception Hierarchy
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    static int Zero = 0;
    static void AFunction()
    {
        int j = 22 / Zero;
        // the following line is never executed.
        Console.WriteLine("In AFunction()");
    }
    public static void Main()
    {
        try
        {
            AFunction();
        }
        catch (DivideByZeroException e)
        {
            Console.WriteLine("DivideByZero {0}", e);
        }
    }
}