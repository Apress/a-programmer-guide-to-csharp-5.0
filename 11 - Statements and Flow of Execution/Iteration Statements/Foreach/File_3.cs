// 12 - Statements and Flow of Execution\Iteration Statements\Foreach
// copyright 2000 Eric Gunnerson
using System;
using System.Collections;
class Test
{
    public static void Main()
    {
        Hashtable    hash = new Hashtable();
        hash.Add("Fred", "Flintstone");
        hash.Add("Barney", "Rubble");
        hash.Add("Mr.", "Slate");
        hash.Add("Wilma", "Flintstone");
        hash.Add("Betty", "Rubble");
        
        foreach (string firstName in hash.Keys)
        {
            Console.WriteLine("{0} {1}", firstName, hash[firstName]);
        }
    }
}