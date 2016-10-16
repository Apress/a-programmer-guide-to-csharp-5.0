// 18 - Properties\Static Properties
// copyright 2000 Eric Gunnerson
class Color
{
    public Color(int red, int green, int blue)
    {
        this.red = red;
        this.green = green;
        this.blue = blue;
    }
    
    int    red;
    int    green;
    int    blue;
    
    public static Color Red
    {
        get
        {
            return(new Color(255, 0, 0));
        }
    }
    public static Color Green
    {
        get
        {
            return(new Color(0, 255, 0));
        }
    }
    public static Color Blue
    {
        get
        {
            return(new Color(0, 0, 255));
        }
    }
}
class Test
{
    static void Main()
    {
        Color background = Color.Red;
    }
}