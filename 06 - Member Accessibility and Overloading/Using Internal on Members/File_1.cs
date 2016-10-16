// 07 - Member Accessibility and Overloading\Using Internal on Members
// copyright 2000 Eric Gunnerson
public class DrawingObjectGroup
{
    public DrawingObjectGroup()
    {
        objects = new DrawingObject[10];
        objectCount = 0;
    }
    public void AddObject(DrawingObject obj)
    {
        if (objectCount < 10)
        {
            objects[objectCount] = obj;
            objectCount++;
        }
    }
    public void Render()
    {
        for (int i = 0; i < objectCount; i++)
        {
            objects[i].Render();
        }
    }
    
    DrawingObject[]    objects;
    int            objectCount;
}
public class DrawingObject
{
    internal void Render() {}
}
class Test
{
    public static void Main()
    {
        DrawingObjectGroup group = new DrawingObjectGroup();
        group.AddObject(new DrawingObject());
    }
}