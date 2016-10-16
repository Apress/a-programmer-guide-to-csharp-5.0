// 16 - Arrays\The System.Array Type\Reverse
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    int[] arr = {5, 6, 7};
        Array.Reverse(arr);
        foreach (int value in arr)
        {
            Console.WriteLine("Value: {0}", value);
        }
    }
}