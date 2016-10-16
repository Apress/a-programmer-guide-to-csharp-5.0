using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class ThreadedWithWait
{
    int number = 1;

    public void Bump()
    {
        int temp = number;
        Thread.Sleep(1);
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
        ThreadedWithWait example = new ThreadedWithWait();
        for (int threadNum = 0; threadNum < 5; threadNum++)
        {
            Thread thread = new Thread(new ThreadStart(example.DoBump));
            thread.Start();
        }
    }
}
