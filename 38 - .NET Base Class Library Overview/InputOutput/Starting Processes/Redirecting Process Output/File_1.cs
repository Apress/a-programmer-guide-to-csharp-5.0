// 32 - .NET Frameworks Overview\InputOutput\Starting Processes\Redirecting Process Output
// copyright 2000 Eric Gunnerson
using System;
using System.Diagnostics;
class Test
{
    public static void Main()
    {
        Process p = new Process();
        p.StartInfo.FileName = "cmd.exe";
        p.StartInfo.Arguments = "/c dir *.cs";
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.Start();
        
        string output = p.StandardOutput.ReadToEnd();
        
        Console.WriteLine("Output:");
    Console.WriteLine(output);    }
}