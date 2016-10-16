// 31 - Interop\Calling Native DLL Functions\Pointers and Declarative Pinning
// copyright 2000 Eric Gunnerson
// file=ReadFileUnsafe.cs
// compile with: csc /unsafe ReadFileUnsafe.cs
using System;
using System.Runtime.InteropServices;
using System.Text;

class FileRead
{
    const uint GENERIC_READ = 0x80000000;
    const uint OPEN_EXISTING = 3;
    IntPtr handle;
    
    public FileRead(string filename)
    {
        // opens the existing file
        handle = CreateFile(    filename,
        GENERIC_READ,
        0, 
        0,
        OPEN_EXISTING,
        0,
        0);
    }
    
    [DllImport("kernel32", SetLastError=true)]
    static extern IntPtr CreateFile(
    string filename,
    uint desiredAccess,
    uint shareMode,
    uint attributes,        // really SecurityAttributes pointer
    uint creationDisposition,
    uint flagsAndAttributes,
    uint templateFile);
    
    [DllImport("kernel32", SetLastError=true)]
    static extern unsafe bool ReadFile(
    IntPtr hFile,
    void* lpBuffer, 
    int nBytesToRead,
    int* nBytesRead,
    int overlapped);
    
    public unsafe int Read(byte[] buffer, int count)
    {
        int n = 0;
        fixed (byte* p = buffer) 
        {
            ReadFile(handle, p, count, &n, 0);
        }
        return n;
    }
}
class Test
{
    public static void Main(string[] args)
    {
        FileRead fr = new FileRead(args[0]);
        
        byte[] buffer = new byte[128];
        ASCIIEncoding e = new ASCIIEncoding();
        
        // loop through, read until done
        Console.WriteLine("Contents");
        while (fr.Read(buffer, 128) != 0)
        {
            Console.Write("{0}", e.GetString(buffer));
        }
    }
}