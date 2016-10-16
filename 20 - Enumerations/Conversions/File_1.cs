// 20 - Enumerations\Conversions
// copyright 2000 Eric Gunnerson
enum Values
{
    A = 1,
    B = 5,
    C = 3,
    D = 42
}
class Test
{
    public static void Main()
    {
        Values v = (Values) 2;
        int ival = (int) v;
    }
}