// 17 - Strings
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
        string s = "Test String";
        
        for (int index = 0; index < s.Length; index++)
        Console.WriteLine("Char: {0}", s[index]);
    }
}