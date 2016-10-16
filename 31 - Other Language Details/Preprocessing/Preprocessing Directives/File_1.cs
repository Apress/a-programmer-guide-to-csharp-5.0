// 26 - Other Language Details\Preprocessing\Preprocessing Directives
// copyright 2000 Eric Gunnerson
#define DEBUGLOG
using System;
class Test
{
    public static void Main()
    {
        #if DEBUGLOG
        Console.WriteLine("In Main - Debug Enabled");
        #else
        Console.WriteLine("In Main - No Debug");
        #endif
    }
}