// 10 - Interfaces\Interfaces and Inheritance
// copyright 2000 Eric Gunnerson
using System;
interface IHelper
{
    void HelpMeNow();
}
public class Base: IHelper
{
    public void HelpMeNow()
    {
        Console.WriteLine("Base.HelpMeNow()");
    }
}
// Does not implement IHelper, though it has the right
// form.
public class Derived: Base
{
    public new void HelpMeNow()
    {
        Console.WriteLine("Derived.HelpMeNow()");
    }
}
class Test
{
    public static void Main()
    {
        Derived der = new Derived();
        der.HelpMeNow();
        IHelper helper = (IHelper) der;
        helper.HelpMeNow();
    }
}