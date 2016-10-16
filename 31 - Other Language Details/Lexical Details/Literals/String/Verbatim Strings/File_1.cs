// 26 - Other Language Details\Lexical Details\Literals\String\Verbatim Strings
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
        string s = @"
        C: Hello, Miss?
        O: What do you mean, 'Miss'?
        C: I'm Sorry, I have a cold. I wish to make a complaint.";
        Console.WriteLine(s);
    }
}