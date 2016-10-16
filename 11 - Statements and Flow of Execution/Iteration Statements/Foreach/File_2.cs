// 12 - Statements and Flow of Execution\Iteration Statements\Foreach
// copyright 2000 Eric Gunnerson
using System;
using System.Collections;
class MyObject
{
}
class Test
{
    public static void Process(ArrayList arr)
    {
        foreach (MyObject current in arr)
        {
            Console.WriteLine("Item: {0}", current);
        }
    }
}