// 36 - Deeper into C#\Deeper Reflection\Listing All the Types in an Assembly
// copyright 2000 Eric Gunnerson
using System;
using System.Reflection;
enum MyEnum
{
    Val1,
    Val2,
    Val3
}
class MyClass
{
}
struct MyStruct
{
}
class Test
{
    public static void Main(String[] args)
    {
        // list all types in the assembly that is passed
        // in as a parameter
        Assembly a = Assembly.LoadFrom (args[0]);
        Type[] types = a.GetTypes();
        
        // look through each type, and write out some information
        // about them.
        foreach (Type t in types)
        {
            Console.WriteLine ("Name: {0}", t.FullName);
            Console.WriteLine ("Namespace: {0}", t.Namespace);
            Console.WriteLine ("Base Class: {0}", t.BaseType.FullName);
        }
    }
}