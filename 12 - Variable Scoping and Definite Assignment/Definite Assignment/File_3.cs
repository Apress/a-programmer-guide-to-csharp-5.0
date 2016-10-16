// 13 - Variable Scoping and Definite Assignment\Definite Assignment
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
        return(String.Format("({0}, {1})", real, imaginary));
    }
    
    public float    real;
    public float    imaginary;
}

class Test
{
    public static void Main()
    {
        Complex    myNumber1;
        Complex    myNumber2;
        Complex    myNumber3;
        
        myNumber1 = new Complex();
        Console.WriteLine("Number 1: {0}", myNumber1);
        
        myNumber2 = new Complex(5.0F, 4.0F);
        Console.WriteLine("Number 2: {0}", myNumber2);
        
        myNumber3.real = 1.5F;
        myNumber3.imaginary = 15F;
        Console.WriteLine("Number 3: {0}", myNumber3);
    }
}