using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LogAddInToFile : ILogger
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
