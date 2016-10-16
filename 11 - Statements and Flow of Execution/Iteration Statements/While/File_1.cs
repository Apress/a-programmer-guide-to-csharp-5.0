// 12 - Statements and Flow of Execution\Iteration Statements\While
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
        int n = 0;
        while (n < 10)
        {
            Console.WriteLine("Number is {0}", n);
            n++;
        }
    }
}