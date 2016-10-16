using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class ThreadedWithLock
{
    int number = 1;

    public void Bump()
    {
        Monitor.Enter(this); 
        int temp = number;
        Thread.Sleep(1);
        number = temp + 2;
        Console.WriteLine("number = {0}", number);
        Monitor.Exit(this);
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
        ThreadedWithLock example = new ThreadedWithLock();
        for (int threadNum = 0; threadNum < 5; threadNum++)
        {
            Thread thread = new Thread(new ThreadStart(example.DoBump));
            thread.Start();
        }
    }
}
