// 16 - Arrays\Multidimensional and Jagged Arrays\Jagged Arrays
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public static void Main()
    {
    int[][] matrix = {new int[5], new int[4], new int[2] };
        matrix[0][3] = 4;
        matrix[1][1] = 8;
        matrix[2][0] = 5;
        
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                Console.WriteLine("matrix[{0}, {1}] = {2}", i, j, matrix[i][j]);
            }
        }		
    }
}