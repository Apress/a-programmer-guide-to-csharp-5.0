
using System.Diagnostics;

class AssertToFileExample
{
    public static void Test()
    {
        Debug.Listeners.Clear();   // remove default listener...
        Debug.Listeners.Add(new ConsoleTraceListener());

        MyClass myClass = new MyClass(1);

        myClass.VerifyState();
    }
}

