// 21 - Attributes\Using Attributes\A Few More Details
// copyright 2000 Eric Gunnerson
using System.Runtime.InteropServices;
class Test
{
    [return: MarshalAs(UnmanagedType.LPWStr)]
    public static extern string GetMessage();
}