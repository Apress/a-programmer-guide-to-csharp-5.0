// 28 - System.Array and the Collection Classes\Sorting and Searching
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    int[]    arr = {5, 1, 10, 33, 100, 4};
        Array.Sort(arr);
        foreach (int v in arr)
        Console.WriteLine("Element: {0}", v);
    }
}