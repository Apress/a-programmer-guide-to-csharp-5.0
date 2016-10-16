// 17 - Strings\String Interning
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
        string s1 = "Hello";
        string s2 = "Hello";
        string s3 = "Hello".Substring(0, 4) + "o";
        
        Console.WriteLine("Str == : {0}", s1 == s2);
        Console.WriteLine("Ref == : {0}", (object) s1 == (object) s2);
        
        Console.WriteLine("Str == : {0}", s1 == s3);
        Console.WriteLine("Ref == : {0}", (object) s1 == (object) s3);
    }
}