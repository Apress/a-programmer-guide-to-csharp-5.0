// 29 - Threading and Asynchronous Operations\Threads\Joining
// copyright 2000 Eric Gunnerson
using System;
using System.Threading;

class ThreadSleeper
{
    int seconds;
    
    private ThreadSleeper(int seconds)
    {
        this.seconds = seconds; 
    }
    
    public void Nap()
    {
        Console.WriteLine("Napping {0} seconds", seconds);
        Thread.Sleep(seconds * 1000);
    }
    
    public static Thread DoSleep(int seconds)
    {
        ThreadSleeper ts = new ThreadSleeper(seconds);
        Thread thread = new Thread(new ThreadStart(ts.Nap));
        thread.Start();
        return(thread);
    }
}

class Test
{
    public static void Main()
    {
        Thread thread = ThreadSleeper.DoSleep(5);
        
        Console.WriteLine("Waiting for thread to join");
        thread.Join();
        Console.WriteLine("Thread Joined");
    }
}