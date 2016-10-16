// 12 - Statements and Flow of Execution\Selection Statements\If
// copyright 2000 Eric Gunnerson
// error
using System;
class Test
{
    public static void Main()
    {
        int    value;
        
        if (value)        // invalid
        System.Console.WriteLine("true");
        
        if (value == 0)    // must use this
        System.Console.WriteLine("true");
    }
}