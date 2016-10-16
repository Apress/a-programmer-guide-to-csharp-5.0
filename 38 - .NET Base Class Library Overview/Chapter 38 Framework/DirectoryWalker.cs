using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public static class DirectoryWalker
{
    public static void DoWalk(
        Action<DirectoryInfo, int> directoryCallback,
        Action<FileInfo, int> fileCallback,
        string rootDirectory)
    {
        DoWalk(
            directoryCallback,
            fileCallback,
            new DirectoryInfo(rootDirectory), 
            0);
    }
    static void DoWalk(
        Action<DirectoryInfo, int> directoryCallback,
        Action<FileInfo, int> fileCallback,
        DirectoryInfo dir, 
        int level)
    {
        foreach (FileInfo file in dir.EnumerateFiles())
        {
            if (fileCallback != null)
            {
                fileCallback(file, level);
            }
        }
        foreach (DirectoryInfo directory in dir.EnumerateDirectories())
        {
            if (directoryCallback != null)
            {
                directoryCallback(directory, level);
            }
            DoWalk(directoryCallback, fileCallback, directory, level + 1);
        }
    }
}
public class DirectoryWalkerTest
{
    public static void PrintDir(DirectoryInfo d, int level)
    {
        Console.WriteLine(new string(' ', level * 2));
        Console.WriteLine("Dir: {0}", d.FullName);
    }
    public static void PrintFile(FileInfo f, int level)
    {
        Console.WriteLine(new string(' ', level * 2));
        Console.WriteLine("File: {0}", f.FullName);
    }
    public static void Mainly()
    {
        DirectoryWalker.DoWalk(
            PrintDir,
            PrintFile,
            "..");
    }
}
