// 08 - Other Class Details\Creation, Initialization, Destruction\Constructors\Private constructors
// copyright 2000 Eric Gunnerson
public class SystemInfo
{
    static SystemInfo cache = null;
    static object cacheLock = new object();
    private SystemInfo()
    {
        // useful stuff here
    }
    
    public static SystemInfo GetSystemInfo()
    {
        lock(cacheLock)
        {
            if (cache == null)
            cache = new SystemInfo();
            
            return(cache);
        }
    }
}