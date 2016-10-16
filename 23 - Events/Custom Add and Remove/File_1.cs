// 23 - Events\Custom Add and Remove
// copyright 2000 Eric Gunnerson
using System;
using System.Collections;
using System.Runtime.CompilerServices;
public class Button
{
    public delegate void ClickHandler(object sender, EventArgs e);
    
    Hashtable delegateStore = new Hashtable();
    static object clickEventKey = new object();
    
    public event ClickHandler Click
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        add
        {
            delegateStore[clickEventKey] =
            Delegate.Combine((Delegate) delegateStore[clickEventKey],
            value);
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        remove
        {
            delegateStore[clickEventKey] =
            Delegate.Remove((Delegate) delegateStore[clickEventKey],
            value);
        }
    }
    
    protected void OnClick()
    {
        ClickHandler ch = (ClickHandler) delegateStore[clickEventKey];
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
    }
}