// 29 - Threading and Asynchronous Operations\Data Protection and Synchronization\A Slightly Broken Example
// copyright 2000 Eric Gunnerson
using System;
using System.Threading;
class Val
{
    int number = 1;
    
    public void Bump()
    {
        int temp = number;
        number = temp + 2;
    }
    
    public override string ToString()
    {
        return(number.ToString());
    }
    
    public void DoBump()
    {
        for (int i = 0; i < 5; i++)
        {
            Bump();
            Console.WriteLine("number = {0}", number);
        }
    }
}

class Test
{
    public static void Main()
    {
        Val v = new Val();
        
        for (int threadNum = 0; threadNum < 5; threadNum++)
        {
            Thread thread = new Thread(new ThreadStart(v.DoBump));
            thread.Start();
        }
    }
}