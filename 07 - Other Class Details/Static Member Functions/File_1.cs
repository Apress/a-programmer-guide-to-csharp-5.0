// 08 - Other Class Details\Static Member Functions
// copyright 2000 Eric Gunnerson
using System;
class MyClass
{
    public MyClass()
    {
        instanceCount++;
    }
    public static int GetInstanceCount()
    {
        return(instanceCount);
    }
    static int instanceCount = 0;
}
class Test
{
    public static void Main()
    {
        MyClass my = new MyClass();
        Console.WriteLine(MyClass.GetInstanceCount());
    }
}