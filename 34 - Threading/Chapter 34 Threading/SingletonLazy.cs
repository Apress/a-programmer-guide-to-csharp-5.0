using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SingletonLazy
{
    private static readonly Lazy<SingletonLazy> s_singleton =
        new Lazy<SingletonLazy>(() => new SingletonLazy() );

    private SingletonLazy()
    {
    }
    public static SingletonLazy Instance
    {
        get { return s_singleton.Value; }
    }
}
