// 20 - Enumerations\The System.Enum Type
// copyright 2000 Eric Gunnerson
using System;

enum Color
{
    red,
    green,
    yellow
}

public class Test
{
    public static void Main()
    {
        Color c = Color.red;
        
        Console.WriteLine("c is {0}", c);
    }
}