using System;
using System.Collections;

class EnvironmentTest
{
    public static void Mainly()
    {
        Console.WriteLine("Command Line: {0}", Environment.CommandLine);
        Console.WriteLine("Current Directory: {0}", Environment.CurrentDirectory);
        Console.WriteLine("Machine Name: {0}", Environment.MachineName);

        Console.WriteLine("Environment Variables");
        foreach (DictionaryEntry var in Environment.GetEnvironmentVariables())
        {
            Console.WriteLine("    {0}={1}", var.Key, var.Value);
        }
    }
}

