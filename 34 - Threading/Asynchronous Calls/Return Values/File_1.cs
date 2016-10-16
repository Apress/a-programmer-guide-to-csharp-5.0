// 29 - Threading and Asynchronous Operations\Asynchronous Calls\Return Values
// copyright 2000 Eric Gunnerson
using System;
using System.Threading;

public class AsyncCaller
{
    public delegate double MathFunctionToCall(double arg);
    
    public void MathCallback(IAsyncResult iar)
    {
        MathFunctionToCall mc = (MathFunctionToCall) iar.AsyncState;
        double result = mc.EndInvoke(iar);
        Console.WriteLine("Function value = {0}", result);
    }
    public void CallMathCallback(MathFunctionToCall mathFunc,
    double start,
    double end,
    double increment)
    {
        AsyncCallback cb = new AsyncCallback(MathCallback);
        
        while (start < end)
        {    
            Console.WriteLine("BeginInvoke: {0}", start);
            mathFunc.BeginInvoke(start, cb, mathFunc);
            start += increment;
        }
    }
}

class Test
{
    public static void Main()
    {
        AsyncCaller ac = new AsyncCaller();
        
        ac.CallMathCallback(new AsyncCaller.MathFunctionToCall(Math.Sin), 0.0, 1.0, 0.2);
        Thread.Sleep(2000);
    }
}