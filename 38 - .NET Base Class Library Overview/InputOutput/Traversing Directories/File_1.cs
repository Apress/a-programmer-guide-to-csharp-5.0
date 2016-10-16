// 32 - .NET Frameworks Overview\InputOutput\Traversing Directories
// copyright 2000 Eric Gunnerson
using System;
using System.IO;

public class DirectoryWalker
{
    public delegate void ProcessDirCallback(DirectoryInfo dir, int level, object obj);
    public delegate void ProcessFileCallback(FileInfo file, int level, object obj);
    
    public DirectoryWalker(    ProcessDirCallback dirCallback,
    ProcessFileCallback fileCallback)
    {
        this.dirCallback = dirCallback;
        this.fileCallback = fileCallback;
    }
    
    public void Walk(string rootDir, object obj)
    {
        DoWalk(new DirectoryInfo(rootDir), 0, obj);
    }
    void DoWalk(DirectoryInfo dir, int level, object obj)
    {
        foreach (FileInfo f in dir.GetFiles())
        {
            if (fileCallback != null)
            fileCallback(f, level, obj);
        }
        foreach (DirectoryInfo d in dir.GetDirectories())
        {
            if (dirCallback != null)
            dirCallback(d, level, obj);
            DoWalk(d, level + 1, obj);
        }
    }
    
    ProcessDirCallback    dirCallback;
    ProcessFileCallback    fileCallback;
}

class Test
{
    public static void PrintDir(DirectoryInfo d, int level, object obj)
    {
        WriteSpaces(level * 2);
        Console.WriteLine("Dir: {0}", d.FullName);
    }
    public static void PrintFile(FileInfo f, int level, object obj)
    {
        WriteSpaces(level * 2);
        Console.WriteLine("File: {0}", f.FullName);
    }
    public static void WriteSpaces(int spaces)
    {
        for (int i = 0; i < spaces; i++)
        Console.Write(" ");
        
    }
    public static void Main(string[] args)
    {
        DirectoryWalker dw = new DirectoryWalker(
        new DirectoryWalker.ProcessDirCallback(PrintDir),
        new DirectoryWalker.ProcessFileCallback(PrintFile));
        
        string root = ".";
        if (args.Length == 1)
        root = args[0];
        dw.Walk(root, "Passed string object");
    }
}