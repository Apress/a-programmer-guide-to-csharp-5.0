using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class MutexExample
{
    const string MyMutexName = "MyProgramMutex";
    public static void Mainly()
    {
        try
        {
            Mutex.OpenExisting(MyMutexName);
            Console.WriteLine("Mutex exists, exiting...");
            return;
        }
        catch (WaitHandleCannotBeOpenedException)
        {
        }

        using (Mutex mutex = new Mutex(true, MyMutexName))
        {
            Console.WriteLine("I have the Mutex");
            Thread.Sleep(10 * 1000);
        }
    }
}
