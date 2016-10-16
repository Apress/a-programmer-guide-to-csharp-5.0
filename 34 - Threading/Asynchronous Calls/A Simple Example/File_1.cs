// 29 - Threading and Asynchronous Operations\Asynchronous Calls\A Simple Example
// copyright 2000 Eric Gunnerson
using System;
public class AsyncCaller
{
    // Declare a delegate that will match Console.WriteLine("string");
    delegate void FuncToCall(string s);
    
    public void CallWriteLine(string s)
    {
        // delegate points to function to call
        // start the async call
        // wait for completion
        FuncToCall func = new FuncToCall(Console.WriteLine);
        IAsyncResult iar = func.BeginInvoke(s, null, null);
        func.EndInvoke(iar);
    }
}

class Test
{
    public static void Main()
    {
        AsyncCaller ac = new AsyncCaller();
        ac.CallWriteLine("Hello");
    }
}