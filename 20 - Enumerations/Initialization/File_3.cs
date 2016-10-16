// 20 - Enumerations\Initialization
// copyright 2000 Eric Gunnerson
enum Values
{
    A = 1,
    B = 2,
    C = A + B,
    D = A * C + 33
}
class Test
{
    public static void Member(Values value)
    {
        // do some processing here
    }
    public static void Main()
    {
        Values value = 0;
        Member(value);
    }
}