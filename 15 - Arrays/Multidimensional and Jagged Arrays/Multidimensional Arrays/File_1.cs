// 16 - Arrays\Multidimensional and Jagged Arrays\Multidimensional Arrays
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    int[,] matrix = { {1, 1}, {2, 2}, {3, 5}, {4, 5}, {134, 44} };
        
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.WriteLine("matrix[{0}, {1}] = {2}", i, j, matrix[i, j]);
            }
        }		
    }
}