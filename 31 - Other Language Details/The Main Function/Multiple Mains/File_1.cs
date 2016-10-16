// 26 - Other Language Details\The Main Function\Multiple Mains
// copyright 2000 Eric Gunnerson
// error 
using System;
class Complex
{
    static int Main()
    {
        // test code here
        Console.WriteLine("Console: Passed");
        return(0);
    }
}
class Test
{
    public static void Main(string[] args)
    {
        foreach (string arg in args)
        Console.WriteLine(arg);
    }
}