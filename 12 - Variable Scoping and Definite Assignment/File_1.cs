// 13 - Variable Scoping and Definite Assignment
// copyright 2000 Eric Gunnerson
// error
using System;
class MyObject
{
    public void Process()
    {
        int    x = 12;
        for (int y = 1; y < 10; y++)
        {
            // no way to name outer x here.
            int x = 14;
            Console.WriteLine("x = {0}", x);
        }
    }
}