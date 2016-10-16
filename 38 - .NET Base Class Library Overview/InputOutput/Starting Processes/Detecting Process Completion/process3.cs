// 32 - .NET Frameworks Overview\InputOutput\Starting Processes\Detecting Process Completion
// copyright 2000 Eric Gunnerson
// file=process3.cs
// compile with csc process3.cs
using System;
using System.Diagnostics;
class Test
{
    static void ProcessDone(object sender, EventArgs e)
    {
        Console.WriteLine("Process Exited");
    }
    
    public static void Main()
    {
        Process p = new Process();
        p.StartInfo.FileName = "notepad.exe";
        p.StartInfo.Arguments = "process3.cs";
        p.EnableRaisingEvents = true;
        p.Exited += new EventHandler(ProcessDone);
        p.Start();
        p.WaitForExit();
        Console.WriteLine("Back from WaitForExit()");
    }
}