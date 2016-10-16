// 30 - Execution-Time Code Generation\Loading Assemblies
// copyright 2000 Eric Gunnerson
// file=LogDriver.cs
// compile with: csc /target:library LogDriver.cs
using System;
using System.Collections;

public interface ILogger
{
    void Log(string message);
}

public class LogDriver
{
    ArrayList loggers = new ArrayList();
    
    public LogDriver()
    {
    }
    
    public void AddLogger(ILogger logger)
    {
        loggers.Add(logger);
    }
    
    public void Log(string message) 
    {
        foreach (ILogger logger in loggers)
        {
            logger.Log(message);
        }
    }
}

public class LogConsole: ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}