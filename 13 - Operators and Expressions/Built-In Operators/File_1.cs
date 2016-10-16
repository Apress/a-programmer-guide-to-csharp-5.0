// 14 - Operators and Expressions\Built-In Operators
// copyright 2000 Eric Gunnerson
// error
class Test
{
    public static void Main()
    {
        short    s1 = 15;
        short    s2 = 16;
        short ssum = (short) (s1 + s2);    // cast is required
        
        int i1 = 15;
        int i2 = 16;
        int isum = i1 + i2;            // no cast required
    }
}