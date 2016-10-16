// 10 - Interfaces\Multiple Implementation\Implementation Hiding
// copyright 2000 Eric Gunnerson
using System;
class DrawingSurface
{
    
}
interface IRenderIcon
{
    void DrawIcon(DrawingSurface surface, int x, int y);
    void DragIcon(DrawingSurface surface, int x, int y, int x2, int y2);
    void ResizeIcon(DrawingSurface surface, int xsize, int ysize);
}
class Employee: IRenderIcon
{
    public Employee(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
    void IRenderIcon.DrawIcon(DrawingSurface surface, int x, int y)
    {
    }
    void IRenderIcon.DragIcon(DrawingSurface surface, int x, int y,
    int x2, int y2)
    {
    }
    void IRenderIcon.ResizeIcon(DrawingSurface surface, int xsize, int ysize)
    {
    }
    int id;
    string name;
}