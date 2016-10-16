// 29 - Threading and Asynchronous Operations\Access Reordering and Volatile\Use of volatile
// copyright 2000 Eric Gunnerson
using System;

class Singleton
{
    static object sync = new object();
    
    static volatile Singleton singleton = null;
    
    private Singleton()
    {
    }
    
    public static Singleton GetSingleton()    
    {
        if (singleton == null)
        {
            lock(sync)
            {
                if (singleton == null)
                singleton = new Singleton();
            }
        }
        
        return(singleton);
    }
}