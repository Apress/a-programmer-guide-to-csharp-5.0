// 29 - Threading and Asynchronous Operations\Asynchronous Calls\Waiting for Completion
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
    
    WaitHandle DoInvoke(MathFunctionToCall mathFunc, double value)
    {
        AsyncCallback cb = new AsyncCallback(MathCallback);
        
        IAsyncResult asyncResult = 
        mathFunc.BeginInvoke(value, cb, mathFunc);
        return(asyncResult.AsyncWaitHandle);
    }
    
    public void CallMathCallback(MathFunctionToCall mathFunc)
    {
        WaitHandle[] waitArray = new WaitHandle[4];
        
        Console.WriteLine("Begin Invoke");
        waitArray[0] = DoInvoke(mathFunc, 0.1);
        waitArray[1] = DoInvoke(mathFunc, 0.5);
        waitArray[2] = DoInvoke(mathFunc, 1.0);
        waitArray[3] = DoInvoke(mathFunc, 3.14159);
        Console.WriteLine("Begin Invoke Done");
        
        Console.WriteLine("Waiting for completion");
        WaitHandle.WaitAll(waitArray, 10000, false);
        Console.WriteLine("Completion achieved");
    }
}

public class Test
{
    public static double DoCalculation(double value)
    {
        Console.WriteLine("DoCalculation: {0}", value);
        Thread.Sleep(250);
        return(Math.Cos(value));
    }
    
    public static void Main()
    {
        AsyncCaller ac = new AsyncCaller();
        
        ac.CallMathCallback(new AsyncCaller.MathFunctionToCall(DoCalculation));
        //Thread.Sleep(500);		// no longer needed
    }
}