using System;
class SingletonLock
{
    static object s_sync = new object();
    static SingletonLock s_singleton = null;

    private SingletonLock()
    {
    }
    public static SingletonLock GetSingleton()
    {
        if (s_singleton == null)
        {
            lock (s_sync)
            {
                if (s_singleton == null)
                {
                    s_singleton = new SingletonLock();
                }
            }
        }
        return (s_singleton);
    }
}

