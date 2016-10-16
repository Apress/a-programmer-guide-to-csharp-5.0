// 29 - Threading and Asynchronous Operations\Threads\Waiting with WaitHandle
// copyright 2000 Eric Gunnerson
using System;
using System.Threading;

class ThreadSleeper
{
    int seconds;
    AutoResetEvent napDone = new AutoResetEvent(false);
    
    private ThreadSleeper(int seconds)
    {
        this.seconds = seconds; 
    }
    
    public void Nap()
    {
        Console.WriteLine("Napping {0} seconds", seconds);
        Thread.Sleep(seconds * 1000);
        Console.WriteLine("{0} second nap finished", seconds);
        napDone.Set();
    }
    
    public static WaitHandle DoSleep(int seconds)
    {
        ThreadSleeper ts = new ThreadSleeper(seconds);
        Thread thread = new Thread(new ThreadStart(ts.Nap));
        thread.Start();
        return(ts.napDone);
    }
}

class Test
{
    public static void Main()
    {
        WaitHandle[] waits = new WaitHandle[2];
        waits[0] = ThreadSleeper.DoSleep(8);
        waits[1] = ThreadSleeper.DoSleep(4);
        
        Console.WriteLine("Waiting for threads to finish");
        WaitHandle.WaitAll(waits);
        Console.WriteLine("Threads finished");
    }
}