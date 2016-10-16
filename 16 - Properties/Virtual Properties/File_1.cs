// 18 - Properties\Virtual Properties
// copyright 2000 Eric Gunnerson
using System;

public abstract class DrawingObject
{
    public abstract string Name
    {
        get;
    }
}
class Circle: DrawingObject
{
    string name = "Circle";
    
    public override string Name
    {
        get
        {
            return(name);
        }
    }
}
class Test
{
    public static void Main()
    {
        DrawingObject d = new Circle();
        Console.WriteLine("Name: {0}", d.Name);
    }
}