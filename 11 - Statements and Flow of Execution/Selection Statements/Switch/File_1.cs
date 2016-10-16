// 12 - Statements and Flow of Execution\Selection Statements\Switch
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public void Process(int i)
    {
        switch (i)
        {
            case 1:
            case 2:
            // code here handles both 1 and 2
            Console.WriteLine("Low Number");
            break;
            
            case 3:
            Console.WriteLine("3");
            goto case 4;
            
            case 4:
            Console.WriteLine("Middle Number");
            break;
            
            default:
            Console.WriteLine("Default Number");
            break;
        }
    }
}