// 04 - Exception Handling\Trying and Catching
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    static int Zero = 0;
    public static void Main()
    {
        // watch for exceptions here
        try
        {
            int j = 22 / Zero;
        }
        // exceptions that occur in try are transferred here
        catch (Exception e)
        {
            Console.WriteLine("Exception " + e.Message);
        }
        Console.WriteLine("After catch");
    }
}