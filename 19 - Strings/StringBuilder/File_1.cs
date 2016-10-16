// 17 - Strings\StringBuilder
// copyright 2000 Eric Gunnerson
using System;
using System.Text;
class Test
{
    public static void Main()
    {
        string s = "I will not buy this record, it is scratched";
    char[] separators = new char[] {' ', ','};
        StringBuilder sb = new StringBuilder();
        int number = 1;
        foreach (string sub in s.Split(separators))
        {
            sb.AppendFormat("{0}: {1} ", number++, sub);
        }
        Console.WriteLine("{0}", sb);
    }
}