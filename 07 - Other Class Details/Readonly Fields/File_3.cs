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
    
    public enum PredefinedEnum
    {
        Red,
        Blue,
        Green
    }
    public static Color GetPredefinedColor(
    PredefinedEnum pre)
    {
        switch (pre)
        {
            case PredefinedEnum.Red:
            return(new Color(255, 0, 0));
            
            case PredefinedEnum.Green:
            return(new Color(0, 255, 0));
            
            case PredefinedEnum.Blue:
            return(new Color(0, 0, 255));
            
            default:
            return(new Color(0, 0, 0));
        }
    }
    int red;
    int blue;
    int green;
}
class Test
{
    static void Main()
    {
        Color background = 
        Color.GetPredefinedColor(Color.PredefinedEnum.Blue);
    }
}