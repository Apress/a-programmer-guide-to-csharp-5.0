// 29 - Threading and Asynchronous Operations\Asynchronous Calls\A Simple Example
// copyright 2000 Eric Gunnerson
using System;

public class AsyncCaller
{
    // Declare a delegate that will match Console.WriteLine("string");
    delegate void FuncToCall(string s);
    
    public void WriteLineCallback(IAsyncResult iar)
    {
        Console.WriteLine("In WriteLineCallback");
        FuncToCall func = (FuncToCall) iar.AsyncState;
        func.EndInvoke(iar);
    }
    
    public void CallWriteLineWithCallback(string s)
    {
        FuncToCall func = new FuncToCall(Console.WriteLine);
        func.BeginInvoke(s, 
        new AsyncCallback(WriteLineCallback), 
        func); // shows up as iar.AsyncState in callback
    }
}
class Test
{
    public static void Main()
    {
        AsyncCaller ac = new AsyncCaller();
        
        ac.CallWriteLineWithCallback("Hello There");
        
        System.Threading.Thread.Sleep(1000);
    }
}