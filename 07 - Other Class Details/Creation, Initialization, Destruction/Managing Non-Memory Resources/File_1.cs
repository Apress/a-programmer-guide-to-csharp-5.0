// 08 - Other Class Details\Creation, Initialization, Destruction\Managing Non-Memory Resources
// copyright 2000 Eric Gunnerson
using System;
using System.Runtime.InteropServices;

class ResourceWrapper
{
    int handle = 0;    
    
    public ResourceWrapper()
    {
        handle = GetWindowsResource();
    }
    
    ~ResourceWrapper()
    {
        FreeWindowsResource(handle);
        handle = 0;
    }
    
    [DllImport("dll.dll")]
    static extern int GetWindowsResource();
    
    [DllImport("dll.dll")]
    static extern void FreeWindowsResource(int handle);
}