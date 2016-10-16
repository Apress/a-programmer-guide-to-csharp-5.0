// 29 - Threading and Asynchronous Operations\Access Reordering and Volatile
// copyright 2000 Eric Gunnerson
using System;
using System.Threading;

class Problem
{
    int x;
    int y;
    int curx;
    int cury;
    
    public Problem()
    {
        x = 0;
        y = 0;
    }
    
    public void Process1()
    {
        x = 1;
        cury = y;
    }
    
    public void Process2()
    {
        y = 1;
        curx = x;
    }
    
    public void TestCurrent()
    {
        Console.WriteLine("curx, cury: {0} {1}", curx, cury);
    }
}

class Test
{
    public static void Main()
    {
        Problem p = new Problem();
        
        Thread t1 = new Thread(new ThreadStart(p.Process1));        
        Thread t2 = new Thread(new ThreadStart(p.Process2));        
        t1.Start();
        t2.Start();
        
        t1.Join();
        t2.Join();
        
        p.TestCurrent();
    }
}