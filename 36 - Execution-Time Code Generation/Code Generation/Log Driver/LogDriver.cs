using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public interface ILogger
{
    void Log(string message);
}
public class LogDriver
{
    List<ILogger> m_loggers = new List<ILogger>();
    public LogDriver()
    {
        ScanDirectoryForLoggers();
    }
    public void AddLogger(ILogger logger)
    {
        m_loggers.Add(logger);
    }
    public void Log(string message)
    {
        foreach (ILogger logger in m_loggers)
        {
            logger.Log(message);
        }
    }

    void ScanDirectoryForLoggers()
    {
        DirectoryInfo dir = new DirectoryInfo(@".");
        foreach (FileInfo f in dir.GetFiles(@"LogAddIn*.dll"))
        {
            ScanAssemblyForLoggers(f.FullName);
        }
    }
    void ScanAssemblyForLoggers(string filename)
    {
        Assembly a = Assembly.LoadFrom(filename);

        foreach (Type t in a.GetTypes())
        {
            if (t.GetInterface("ILogger") != null)
            {
                ILogger iLogger = (ILogger)Activator.CreateInstance(t);
                m_loggers.Add(iLogger);
            }
        }
    }

}
public class LogConsole : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}

