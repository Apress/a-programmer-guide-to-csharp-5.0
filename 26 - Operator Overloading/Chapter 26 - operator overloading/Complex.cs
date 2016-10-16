using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_26___operator_overloading
{
struct Complex
{
    public float m_real;
    public float m_imaginary;

    public Complex(float real, float imaginary)
    {
        m_real = real;
        m_imaginary = imaginary;
    }
    public float Real 
    { 
        get { return m_real; }
        set { m_real = value; }
    }
    public float Imaginary 
    {
        get { return m_imaginary; }
        set { m_imaginary = value; }
    }
    public override string ToString()
    {
        return (String.Format("({0}, {1}i)", m_real, m_imaginary));
    }
    public static bool operator ==(Complex c1, Complex c2)
    {
        return (c1.m_real == c2.m_real) &&
               (c1.m_imaginary == c2.m_imaginary);
    }
    public static bool operator !=(Complex c1, Complex c2)
    {
        return !(c1 == c2);
    }
    public override bool Equals(object o2)
    {
        return this == (Complex) o2;
    }
    public override int GetHashCode()
    {
        return m_real.GetHashCode() ^ m_imaginary.GetHashCode();
    }
    public static Complex operator +(Complex c1, Complex c2)
    {
        return new Complex(c1.m_real + c2.m_real, c1.m_imaginary + c2.m_imaginary);
    }

    public static Complex operator -(Complex c1, Complex c2)
    {
        return new Complex(c1.m_real - c2.m_real, c1.m_imaginary - c2.m_imaginary);
    }

    // product of two complex numbers
    public static Complex operator *(Complex c1, Complex c2)
    {
        return new Complex(c1.m_real * c2.m_real - c1.m_imaginary * c2.m_imaginary,
                           c1.m_real * c2.m_imaginary + c2.m_real * c1.m_imaginary);
    }

    // quotient of two complex numbers
    public static Complex operator /(Complex c1, Complex c2)
    {
        if ((c2.m_real == 0.0f) &&
            (c2.m_imaginary == 0.0f))
        {

            throw new DivideByZeroException("Can't divide by zero Complex number");
        }

        float newReal =
            (c1.m_real * c2.m_real + c1.m_imaginary * c2.m_imaginary) /
            (c2.m_real * c2.m_real + c2.m_imaginary * c2.m_imaginary);
        float newImaginary =
            (c2.m_real * c1.m_imaginary - c1.m_real * c2.m_imaginary) /
            (c2.m_real * c2.m_real + c2.m_imaginary * c2.m_imaginary);

        return new Complex(newReal, newImaginary);
    }

    // non-operator versions for other languages...
    public static Complex Add(Complex c1, Complex c2)
    {
        return c1 + c2;
    }

    public static Complex Subtract(Complex c1, Complex c2)
    {
        return c1 - c2;
    }

    public static Complex Multiply(Complex c1, Complex c2)
    {
        return c1 * c2;
    }

    public static Complex Divide(Complex c1, Complex c2)
    {
        return c1 / c2;
    }
}

class Test
{
    public static void Main()
    {
        Complex c1 = new Complex(3, 1);
        Complex c2 = new Complex(1, 2);

        Console.WriteLine("c1 == c2: {0}", c1 == c2);
        Console.WriteLine("c1 != c2: {0}", c1 != c2);
        Console.WriteLine("c1 + c2 = {0}", c1 + c2);
        Console.WriteLine("c1 - c2 = {0}", c1 - c2);
        Console.WriteLine("c1 * c2 = {0}", c1 * c2);
        Console.WriteLine("c1 / c2 = {0}", c1 / c2);
    }
}

}
