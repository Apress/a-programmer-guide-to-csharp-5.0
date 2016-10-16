// 26 - Other Language Details\The Main Function\Command-Line Parameters
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main(string[] args)
    {
        foreach (string arg in args)
        Console.WriteLine("Arg: {0}", arg);
    }
}