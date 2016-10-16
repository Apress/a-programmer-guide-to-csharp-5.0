// 16 - Arrays\Array Conversions
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void PrintArray(object[] arr)
    {
        foreach (object obj in arr)
        Console.WriteLine("Word: {0}", obj);
    }
    public static void Main()
    {
        string s = "I will not buy this record, it is scratched.";
    char[] separators = {' '};
        string[] words = s.Split(separators);
        PrintArray(words);
    }
}