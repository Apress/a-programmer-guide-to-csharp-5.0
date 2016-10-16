// 13 - Variable Scoping and Definite Assignment\Definite Assignment\Definite Assignment and Arrays
// copyright 2000 Eric Gunnerson
using System;
struct Complex
{
    public Complex(float real, float imaginary) 
    {
        this.real = real;
        this.imaginary = imaginary;
    }
    public override string ToString()
    {
        return(String.Format("({0}, {0})", real, imaginary));
    }
    
    public float    real;
    public float    imaginary;
}

class Test
{
    public static void Main()
    {
        Complex[]    arr = new Complex[10];
        Console.WriteLine("Element 5: {0}", arr[5]);        // legal
    }
}