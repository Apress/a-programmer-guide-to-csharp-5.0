// 17 - Strings\An Example
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
        string s = "Oh, I hadn't thought of that";
    char[] separators = new char[] {' ', ','};
        foreach (string sub in s.Split(separators))
        {
            Console.WriteLine("Word: {0}", sub);
        }
    }
}