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
        for (int nIndex = 0; nIndex < arr.Count; nIndex++)
        {
            // cast is required by ArrayList stores
            // object references
            MyObject current = (MyObject) arr[nIndex];
            Console.WriteLine("Item: {0}", current);
        }
    }
}