// 32 - .NET Frameworks Overview\Accessing Environment Settings
// copyright 2000 Eric Gunnerson
using System;
using System.Collections;

class Test
{
    public static void Main()
    {
        Console.WriteLine("Command Line: {0}", Environment.CommandLine);
        Console.WriteLine("Current Directory: {0}", Environment.CurrentDirectory);
        Console.WriteLine("Machine Name: {0}", Environment.MachineName);
        Console.WriteLine("OS Version: {0}", Environment.OSVersion);
        Console.WriteLine("Stack Trace: {0}", Environment.StackTrace);
        Console.WriteLine("System Directory: {0}", Environment.SystemDirectory);
        Console.WriteLine("Tick Count: {0}", Environment.TickCount);
        Console.WriteLine("Version: {0}", Environment.Version);
        Console.WriteLine("Working Set: {0}", Environment.WorkingSet);
        
        Console.WriteLine("Environment Variables");
        foreach (DictionaryEntry var in Environment.GetEnvironmentVariables())
        Console.WriteLine("    {0}={1}", var.Key, var.Value);
        
        Console.WriteLine("Logical Drives");
        foreach (string drive in Environment.GetLogicalDrives())
        Console.WriteLine("    {0}", drive);
    }
}