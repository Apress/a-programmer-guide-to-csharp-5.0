// 10 - Interfaces\Multiple Implementation
// copyright 2000 Eric Gunnerson
// error
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
    // IFoo or IBar implementation?
    public void Execute() {}
}