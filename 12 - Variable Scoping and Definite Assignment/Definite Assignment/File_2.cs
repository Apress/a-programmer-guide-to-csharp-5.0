// 13 - Variable Scoping and Definite Assignment\Definite Assignment
// copyright 2000 Eric Gunnerson
// error
using System;
class MyClass
{
    public MyClass(int value) 
    {
        this.value = value;
    }
    public int Calculate() 
    {
        return(value * 10);
    }
    public int    value;
}
class Test
{    
    public static void Main()
    {
        MyClass mine;
        
        Console.WriteLine("{0}", mine.value);            // error
        Console.WriteLine("{0}", mine.Calculate());        // error
        mine = new MyClass(12);
        Console.WriteLine("{0}", mine.value);        // okay now
    }
}