using System.Diagnostics;

class MyClass
{
    public MyClass(int i)
    {
        m_i = i;
    }

    public void VerifyState()
    {
        Debug.Assert(m_i == 0, "Bad State");
    }

    int m_i = 0;
}
