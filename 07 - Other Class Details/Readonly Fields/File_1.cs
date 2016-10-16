// 08 - Other Class Details\Readonly Fields
// copyright 2000 Eric Gunnerson
// error
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
    // call to new can't be used with static
    public const Color Red = new Color(255, 0, 0);
    public const Color Green = new Color(0, 255, 0);
    public const Color Blue = new Color(0, 0, 255);
}
class Test
{
    static void Main()
    {
        Color background = Color.Red;
    }
}