// 36 - Deeper into C#\Deeper Reflection\Invoking Functions
// copyright 2000 Eric Gunnerson
// file=process1.cs
// compile with: csc /target:library process1.cs iprocess.cs
using System;
namespace MamaSoft
{
    class Processor1: IProcess
    {
        Processor1() {}
        
        public string Process(int param)
        {
            Console.WriteLine("In Processor1.Process(): {0}", param);
            return("Raise the mainsail! ");
        }
    }
}