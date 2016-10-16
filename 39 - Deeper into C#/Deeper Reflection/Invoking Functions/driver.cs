// 36 - Deeper into C#\Deeper Reflection\Invoking Functions
// copyright 2000 Eric Gunnerson
// file=driver.cs
// compile with: csc driver.cs iprocess.cs
using System;
using System.Reflection;
using MamaSoft;
class Test
{
    public static void ProcessAssembly(string aname)
    {
        Console.WriteLine("Loading: {0}", aname);
        Assembly a = Assembly.LoadFrom (aname);
        
        // walk through each type in the assembly
        foreach (Type t in a.GetTypes())
        {
            // if it’s a class, it might be one that we want.
            if (t.IsClass)
            {
                Console.WriteLine("  Found Class: {0}", t.FullName);
                
                // check to see if it implements IProcess
                if (t.GetInterface("IProcess") == null)
                continue;
                
                // it implements IProcess. Create an instance 
                // of the object.
                object o = Activator.CreateInstance(t);
                
                // create the parameter list, call it,
                // and print out the return value.
                Console.WriteLine("    Calling Process() on {0}", 
                t.FullName);
            object[] args = new object[] {55};
                object result;
                result = t.InvokeMember("Process",
                BindingFlags.Default |
                BindingFlags.InvokeMethod, 
                null, o, args);
                Console.WriteLine("    Result: {0}", result);
            }
        }
    }
    public static void Main(String[] args)
    {
        foreach (string arg in args)
        ProcessAssembly(arg);
    }
}