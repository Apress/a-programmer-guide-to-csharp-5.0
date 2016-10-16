using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_16___Generic_Types
{
interface IFirstItem<out T>  // remove 'out' to show the compiler error.
{
    T GetFirstItem();
    //void NotLegal(T parameter); // also generates an error.
}
class MyFirstList<T> : List<T>, IFirstItem<T>
{
    public MyFirstList() { }

    public T GetFirstItem()
    {
        return this[0];
    }
}

    class GenericCollectionCovariance
    {
        void Example()
        {
            MyFirstList<Sedan> sedans = new MyFirstList<Sedan>();
            sedans.Add(new Sedan());

            TestService();
        }

void TestService()
{
    MyFirstList<Sedan> sedans = new MyFirstList<Sedan>();
    sedans.Add(new Sedan());

    PerformService(sedans);
}
void PerformService(IFirstItem<Auto> autos)
{
}
    }
}
