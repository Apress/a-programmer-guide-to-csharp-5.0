// 15 - Conversions\Conversions of Classes (Reference Types)\To an Interface the Object Might Implement
// copyright 2000 Eric Gunnerson
using System;
interface IDebugDump
{
    string DumpObject();
}
class Simple
{
    public Simple(int value)
    {
        this.value = value;
    }
    public override string ToString()
    {
        return(value.ToString());
    }
    int value;
}
class Complicated: IDebugDump
{
    public Complicated(string name)
    {
        this.name = name;
    }
    public override string ToString()
    {
        return(name);
    }
    string IDebugDump.DumpObject()
    {
        return(String.Format(
        "{0}\nLatency: {1}\nRequests: {2}\nFailures: {3}\n",
    new object[] {name,    latency, requestCount, failedCount} ));
    }
    string name;
    int latency = 0;
    int requestCount = 0;
    int failedCount = 0;
}
class Test
{
    public static void DoConsoleDump(params object[] arr)
    {
        foreach (object o in arr)
        {
            IDebugDump dumper = o as IDebugDump;
            if (dumper != null)
            Console.WriteLine("{0}", dumper.DumpObject());
            else
            Console.WriteLine("{0}", o);
        }
    }
    public static void Main()
    {
        Simple s = new Simple(13);
        Complicated c = new Complicated("Tracking Test");
        DoConsoleDump(s, c);
    }
}