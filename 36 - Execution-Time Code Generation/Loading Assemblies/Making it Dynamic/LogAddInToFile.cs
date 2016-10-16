// 30 - Execution-Time Code Generation\Loading Assemblies\Making it Dynamic
// copyright 2000 Eric Gunnerson
// file=LogAddInToFile.cs
// compile with: csc /r:..\logdriver.dll /target:library logaddintofile.cs
using System;
using System.Collections;
using System.IO;

public class LogAddInToFile: ILogger
{
    StreamWriter streamWriter;
    
    public LogAddInToFile()
    {
        streamWriter = File.CreateText(@"logger.log");
        streamWriter.AutoFlush = true;
    }
    
    public void Log(string message)
    {
        streamWriter.WriteLine(message);
    }
}