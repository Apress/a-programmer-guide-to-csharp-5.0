using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BaseSynchronous
{
    int number = 1;

    public void Bump()
    {
        int temp = number;
        number = temp + 2;
        Console.WriteLine("number = {0}", number);
    }

    public override string ToString()
    {
        return (number.ToString());
    }

    public void DoBump()
    {
        for (int i = 0; i < 5; i++)
        {
            Bump();
        }
    }
    public static void Mainly()
    {
        BaseSynchronous example = new BaseSynchronous();

        example.DoBump();
    }
}




