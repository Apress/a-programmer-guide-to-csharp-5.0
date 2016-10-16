// 12 - Statements and Flow of Execution\Iteration Statements\For
// copyright 2000 Eric Gunnerson
// error
using System;
class Test
{
    public static void Main()
    {
        for (int n = 0; n < 10; n++)
        {
            if (n == 8)
            break;
            Console.WriteLine("Number is {0}", n);
        }
        // error; n is out of scope
        Console.WriteLine("Last Number is {0}", n);
    }
}