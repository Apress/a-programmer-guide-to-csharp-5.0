// 12 - Statements and Flow of Execution\Iteration Statements\Do
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
        int n = 0;
        do
        {
            Console.WriteLine("Number is {0}", n);
            n++;
        } while (n < 10);
    }
}