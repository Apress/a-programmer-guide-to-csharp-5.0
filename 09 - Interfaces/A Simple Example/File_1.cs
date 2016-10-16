// 10 - Interfaces\A Simple Example
// copyright 2000 Eric Gunnerson
public class DiagramObject
{
    public DiagramObject() {}
}

interface IScalable
{
    void ScaleX(float factor);
    void ScaleY(float factor);
}
// A diagram object that also implements IScalable
public class TextObject: DiagramObject, IScalable
{
    public TextObject(string text)
    {
        this.text = text;
    }
    // implementing IScalable.ScaleX()
    public void ScaleX(float factor)
    {
        // scale the object here.
    }
    
    // implementing IScalable.ScaleY()
    public void ScaleY(float factor)
    {
        // scale the object here.
    }
    
    private string text;
}

class Test
{
    public static void Main()
    {
        TextObject text = new TextObject("Hello");
        
        IScalable scalable = (IScalable) text;
        scalable.ScaleX(0.5F);
        scalable.ScaleY(0.5F);
    }
}