// 36 - Deeper into C#\Deeper Reflection\Invoking Functions
// copyright 2000 Eric Gunnerson
// file=process2.cs
// compile with: csc /target:library process2.cs iprocess.cs
using System;
namespace MamaSoft
{
    class Processor2: IProcess
    {
        Processor2() {}
        
        public string Process(int param)
        {
            Console.WriteLine("In Processor2.Process(): {0}", param);
            return("Shiver me timbers! ");
        }
    }
    class Unrelated
    {
    }
}