using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_34_Threading
{
class SingletonIndustrious
{
    private static readonly SingletonIndustrious s_instance = new SingletonIndustrious();

    private SingletonIndustrious() { }  // nobody can call this

    public static SingletonIndustrious Instance
    {
        get { return s_instance; }
    }
}

}
