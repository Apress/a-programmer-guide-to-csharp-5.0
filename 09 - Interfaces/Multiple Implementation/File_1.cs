// 10 - Interfaces\Multiple Implementation
// copyright 2000 Eric Gunnerson
interface IFoo
{
    void ExecuteFoo();
}

interface IBar
{
    void ExecuteBar();
}

class Tester: IFoo, IBar
{
    public void ExecuteFoo() {}
    public void ExecuteBar() {}
}