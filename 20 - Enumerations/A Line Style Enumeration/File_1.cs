// 20 - Enumerations\A Line Style Enumeration
// copyright 2000 Eric Gunnerson
using System;
public class Draw
{
    public enum LineStyle
    {
        Solid,
        Dotted,
        DotDash,		// trailing comma is optional
    }
    
    public void DrawLine(int x1, int y1, int x2, int y2, LineStyle lineStyle)
    {
        switch (lineStyle)
        {
            case LineStyle.Solid:
            // draw solid
            break;
            
            case LineStyle.Dotted:
            // draw dotted
            break;
            
            case LineStyle.DotDash:
            // draw dotdash
            break;
            
            default:
            throw(new ArgumentException("Invalid line style"));
        }
    }
}
class Test
{
    public static void Main()
    {
        Draw draw = new Draw();
        draw.DrawLine(0, 0, 10, 10, Draw.LineStyle.Solid);
        draw.DrawLine(5, 6, 23, 3, (Draw.LineStyle) 35);
    }
}