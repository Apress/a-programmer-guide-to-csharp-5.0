// 15 - Conversions\Numeric Types
// copyright 2000 Eric Gunnerson
class Test
{
    public static void Main()
    {
        // all implicit
        sbyte v = 55;
        short v2 = v;
        int v3 = v2;
        long v4 = v3;
        
        // explicit to "smaller" types
        v3 = (int) v4;
        v2 = (short) v3;
        v = (sbyte) v2;
    }
}