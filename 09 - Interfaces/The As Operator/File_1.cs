// 10 - Interfaces\The As Operator
// copyright 2000 Eric Gunnerson
using System;
interface IScalable
{
    void ScaleX(float factor);
    void ScaleY(float factor);
}
public class DiagramObject
{
    public DiagramObject() {}
}
public class TextObject: DiagramObject, IScalable
{
    public TextObject(string text)
    {
        this.text = text;
    }
    // implementing IScalable.ScaleX()
    public void ScaleX(float factor)
    {
        Console.WriteLine("ScaleX: {0} {1}", text, factor);
        // scale the object here.
    }
    
    // implementing IScalable.ScaleY()
    public void ScaleY(float factor)
    {
        Console.WriteLine("ScaleY: {0} {1}", text, factor);
        // scale the object here.
    }
    
    private string text;
}
class Test
{
    public static void Main()
    {
        DiagramObject[] dArray = new DiagramObject[100];
        
        dArray[0] = new DiagramObject();
        dArray[1] = new TextObject("Text Dude");
        dArray[2] = new TextObject("Text Backup");
        
        // array gets initialized here, with classes that
        // derive from DiagramObject. Some of them implement
        // IScalable.
        
        foreach (DiagramObject d in dArray)
        {
            IScalable scalable = d as IScalable;
            if (scalable != null)
            {
                scalable.ScaleX(0.1F);
                scalable.ScaleY(10.0F);
            }
        }
    }
}