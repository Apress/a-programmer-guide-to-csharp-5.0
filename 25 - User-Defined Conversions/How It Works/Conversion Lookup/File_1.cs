// 24 - User-Defined Conversions\How It Works\Conversion Lookup
// copyright 2000 Eric Gunnerson
public class S
{
    public static implicit operator T(S s) 
    { 
        // conversion here
        return(new T());
    }
}

public class TBase
{
}

public class T: TBase
{
    
}
public class Test
{
    public static void Main()
    {
        S myS = new S();
        TBase tb = (TBase) myS;
    }
}