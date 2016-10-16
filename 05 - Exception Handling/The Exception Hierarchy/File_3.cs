// 04 - Exception Handling\The Exception Hierarchy
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    static int Zero = 0;
    static void AFunction()
    {
        try
        {
            int j = 22 / Zero;
        }
        // this exception doesn't match
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine("OutOfRangeException: {0}", e);
        }
        Console.WriteLine("In AFunction()");
    }
    public static void Main()
    {
        try
        {
            AFunction();
        }
        // this exception doesn't match
        catch (ArgumentException e)
        {
            Console.WriteLine("ArgumentException {0}", e);
        }
    }
}