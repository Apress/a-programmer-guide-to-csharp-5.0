using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

// file=ReadFileUnsafe.cs
// compile with: csc /unsafe ReadFileUnsafe.cs
using System.Runtime.InteropServices;

class FileRead
{
    const uint GENERIC_READ = 0x80000000;
    const uint OPEN_EXISTING = 3;
    SafeFileHandle handle;

    public FileRead(string filename)
    {
        // opens the existing file...
        handle = CreateFile(filename,
                GENERIC_READ,
                0,
                0,
                OPEN_EXISTING,
                0,
                0);
    }

    [DllImport("kernel32", SetLastError = true)]
    static extern SafeFileHandle CreateFile(
        string filename,
        uint desiredAccess,
        uint shareMode,
        uint attributes,        // really SecurityAttributes pointer
        uint creationDisposition,
        uint flagsAndAttributes,
        uint templateFile);

    // SetLastError =true is used to tell the interop layer to keep track of
    //underlying Windows errors
    [DllImport("kernel32", SetLastError = true)]
    static extern unsafe bool ReadFile(
        SafeFileHandle hFile,
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
class ReadFileUnsafeTest
{
    public static void Mainly()
    {
        FileRead fr = new FileRead(@"..\..\readfileunsafe.cs");

        byte[] buffer = new byte[128];
        ASCIIEncoding e = new ASCIIEncoding();

        // loop through, read until done...
        Console.WriteLine("Contents");
        int bytesRead = 0;
        while ((bytesRead = fr.Read(buffer, buffer.Length - 1)) != 0)
        {
            Console.Write("{0}", e.GetString(buffer, 0, bytesRead));
        }
    }
}

