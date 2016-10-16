// 10 - Interfaces\Multiple Implementation\Explicit Interface Implementation
// copyright 2000 Eric Gunnerson
using System;
interface IFoo
{
    void Execute();
}

interface IBar
{
    void Execute();
}

class Tester: IFoo, IBar
{
    void IFoo.Execute() 
    {
        Console.WriteLine("IFoo.Execute implementation");
    }
    void IBar.Execute()
    {
        Console.WriteLine("IBar.Execute implementation");
    }
}

class Test
{
    public static void Main()
    {
        Tester tester = new Tester();
        
        IFoo iFoo = (IFoo) tester;
        iFoo.Execute();
        
        IBar iBar = (IBar) tester;
        iBar.Execute();
    }
}