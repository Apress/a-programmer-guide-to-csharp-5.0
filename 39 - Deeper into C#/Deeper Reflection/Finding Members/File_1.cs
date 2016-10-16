// 36 - Deeper into C#\Deeper Reflection\Finding Members
// copyright 2000 Eric Gunnerson
using System;
using System.Reflection;
class MyClass
{
    MyClass() {}
    static void Process()
    {
    }
    public int DoThatThing(int i, Decimal d, string[] args)
    {
        return(55);
    }
    public int        value = 0;
    public float        log = 1.0f;
    public static int    value2 = 44;
}
class Test
{    
    public static void Main(String[] args)
    {
        // iterate through the fields of the class
        Console.WriteLine("Fields of MyClass");
        Type t = typeof (MyClass);
        foreach (MemberInfo m in t.GetFields())
        {
            Console.WriteLine("{0}", m);
        }
        
        // and iterate through the methods of the class
        Console.WriteLine("Methods of MyClass");
        foreach (MethodInfo m in t.GetMethods())
        {
            Console.WriteLine("{0}", m);
            foreach (ParameterInfo p in m.GetParameters())
            {
                Console.WriteLine("  Param: {0} {1}",
                p.ParameterType, p.Name);
            }
        }
    }
}