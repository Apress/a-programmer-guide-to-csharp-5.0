// 12 - Statements and Flow of Execution\Selection Statements\Switch
// copyright 2000 Eric Gunnerson
using System;
class Test
{
    public void Process(string htmlTag)
    {
        switch (htmlTag)
        {
            case "P":
            Console.WriteLine("Paragraph start");
            break;
            case "DIV":
            Console.WriteLine("Division");
            break;
            case "FORM":
            Console.WriteLine("Form Tag");
            break;
            default:
            Console.WriteLine("Unrecognized tag");
            break;
        }
    }
}