using System;
using System.Threading;

class Problem
{
    int m_x;
    int m_y;
    int m_curx;
    int m_cury;

    public Problem()
    {
        m_x = 0;
        m_y = 0;
    }

    public void Process1()
    {
        m_x = 1;
        m_cury = m_y;
    }

    public void Process2()
    {
        m_y = 1;
        m_curx = m_x;
    }

    public void TestCurrent()
    {
        Console.WriteLine("curx, cury: {0} {1}", m_curx, m_cury);
    }
}

class ReorderingTest
{
    public static void Mainly()
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
