// 04 - Exception Handling\The Exception Hierarchy
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    static int Zero = 0;
    public static void Main()
    {
        try
        {
            int j = 22 / Zero;
        }
        // catch a specific exception
        catch (DivideByZeroException e)
        {
            Console.WriteLine("DivideByZero {0}", e);
        }
        // catch any remaining exceptions
        catch (Exception e)
        {
            Console.WriteLine("Exception {0}", e);
        }
    }
}