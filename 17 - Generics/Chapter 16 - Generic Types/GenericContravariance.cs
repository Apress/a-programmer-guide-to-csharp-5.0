
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_16___Generic_Types
{
interface IEqual<T>
{
    bool IsEqual(T x, T y);
}
class Comparer : IEqual<object>
{
    public bool IsEqual(object x, object y)
    {
        return true;
    }
}
class GenericContravariance
{
    void Example()
    {
        Comparer comparer = new Comparer();
        TestEquality(comparer);
    }
    void TestEquality(IEqual<Auto> equalizer)
    {
    }
}
}
