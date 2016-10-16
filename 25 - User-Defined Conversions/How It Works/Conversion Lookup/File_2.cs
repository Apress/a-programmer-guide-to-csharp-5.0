// 24 - User-Defined Conversions\How It Works\Conversion Lookup
// copyright 2000 Eric Gunnerson
// error
class S
{
}
class TBase
{
}
class T: TBase
{
    public static implicit operator T(S s) 
    {
        return(new T());
    }
}
class Test
{
    public static void Main()
    {
        S myS = new S();
        TBase tb = (TBase) myS;
    }
}