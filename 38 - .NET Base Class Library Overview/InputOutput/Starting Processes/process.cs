// 32 - .NET Frameworks Overview\InputOutput\Starting Processes
// copyright 2000 Eric Gunnerson
// file=process.cs
// compile with csc process.cs
using System.Diagnostics;
class Test
{
    public static void Main()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "notepad.exe";
        startInfo.Arguments = "process.cs";
        
        Process.Start(startInfo);
    }
}