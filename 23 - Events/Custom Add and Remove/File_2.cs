// 23 - Events\Custom Add and Remove
// copyright 2000 Eric Gunnerson
using System;
using System.Collections;
using System.Runtime.CompilerServices;

//
// Global delegate cache. Uses a two-level hashtable. The delegateStore hashtable 
// stores a hashtable keyed on the object instance, and the instance hashtable is 
// keyed on the unique key. This allows fast tear-down of the object when it's destroyed.
//
public class DelegateCache
{
    private DelegateCache() {}    // nobody can create one of these
    
    Hashtable delegateStore = new Hashtable();    // top level hash table
    
    static DelegateCache dc = new DelegateCache();    // our single instance
    
    Hashtable GetInstanceHash(object instance)
    {
        Hashtable instanceHash = (Hashtable) delegateStore[instance];
        
        if (instanceHash == null)
        {
            instanceHash = new Hashtable();
            delegateStore[instance] = instanceHash;
            
        }
        return(instanceHash);
    }
    
    public static void Combine(Delegate myDelegate, object instance, object key)
    {
        lock(instance)
        {
            Hashtable instanceHash = dc.GetInstanceHash(instance);        
            
            instanceHash[key] = Delegate.Combine((Delegate) instanceHash[key],
            myDelegate);
        }
    }
    
    public static void Remove(Delegate myDelegate, object instance, object key)
    {
        lock(instance)
        {
            Hashtable instanceHash = dc.GetInstanceHash(instance);        
            
            instanceHash[key] = Delegate.Remove((Delegate) instanceHash[key],
            myDelegate);
        }
    }
    
    public static Delegate Fetch(object instance, object key)
    {
        Hashtable instanceHash = dc.GetInstanceHash(instance);        
        
        return((Delegate) instanceHash[key]);
    }
    
    public static void ClearDelegates(object instance)
    {
        dc.delegateStore.Remove(instance);
    }
}

public class Button
{
    public void TearDown()
    {
        DelegateCache.ClearDelegates(this);
    }
    
    
    public delegate void ClickHandler(object sender, EventArgs e);
    
    static object clickEventKey = new object();
    
    public event ClickHandler Click
    {
        add
        {
            DelegateCache.Combine(value, this, clickEventKey);
        }
        
        remove
        {
            DelegateCache.Remove(value, this, clickEventKey);
        }
    }
    
    protected void OnClick()
    {
        ClickHandler ch = (ClickHandler) DelegateCache.Fetch(this, clickEventKey);
        
        if (ch != null)
        ch(this, null);
    }
    
    public void SimulateClick()
    {
        OnClick();
    }
}

class Test
{
    static public void ButtonHandler(object sender, EventArgs e)
    {
        Console.WriteLine("Button clicked");
    }
    
    public static void Main()
    {
        Button button = new Button();
        
        button.Click += new Button.ClickHandler(ButtonHandler);
        
        button.SimulateClick();
        
        button.Click -= new Button.ClickHandler(ButtonHandler);
        
        button.TearDown();
    }
}