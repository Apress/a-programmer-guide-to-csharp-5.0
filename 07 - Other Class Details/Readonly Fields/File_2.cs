// 08 - Other Class Details\Readonly Fields
// copyright 2000 Eric Gunnerson
class Color
{
    public Color(int red, int green, int blue)
    {
        this.red = red;
        this.green = green;
        this.blue = blue;
    }
    
    int red;
    int green;
    int blue;
    
    public static readonly Color Red;
    public static readonly Color Green;
    public static readonly Color Blue;
    
    // static constructor
    static Color()
    {
        Red = new Color(255, 0, 0);
        Green = new Color(0, 255, 0);
        Blue = new Color(0, 0, 255);
    }
}
class Test
{
    static void Main()
    {
        Color background = Color.Red;
    }
}