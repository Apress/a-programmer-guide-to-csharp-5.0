// 26 - Other Language Details\Preprocessing\Preprocessing Directives
// copyright 2000 Eric Gunnerson
// error
using System;
class Test
{
    #define DEBUGLOG
    public static void Main()
    {
        #if DEBUGLOG
        Console.WriteLine("In Main - Debug Enabled");
        #else
        Console.WriteLine("In Main - No Debug");
        #endif
    }
}