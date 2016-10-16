// 08 - Other Class Details\Creation, Initialization, Destruction\Managing Non-Memory Resources
// copyright 2000 Eric Gunnerson
using System;
using System.Runtime.InteropServices;

class ResourceWrapper: IDisposable
{
    int handle = 0;    
    
    public ResourceWrapper()
    {
        handle = GetWindowsResource();
    }
    
    // does cleanup for this object only
    void DoDispose()
    {
        FreeWindowsResource(handle);
        handle = 0;
    }
    
    ~ResourceWrapper()
    {
        DoDispose();
    }
    
    // dispose cleans up its object, and any objects it holds
    // that also implement IDisposable. 
    public void Dispose()
    {
        DoDispose();
        // call Dispose() on our base class (if necessary), and 
        // on any other resources we hold that implement IDisposable
        
        GC.SuppressFinalize(this);  
    }
    
    [DllImport("dll.dll")]
    static extern int GetWindowsResource();
    
    [DllImport("dll.dll")]
    static extern void FreeWindowsResource(int handle);
}