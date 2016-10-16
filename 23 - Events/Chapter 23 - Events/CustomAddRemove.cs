using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_23___Events
{
using System;
using System.Collections;
using System.Runtime.CompilerServices;
public class Button
{
    Dictionary<object, EventHandler> m_delegateStore = new Dictionary<object, EventHandler>();
    static object clickEventKey = new object();

    public event EventHandler Click
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        add
        {
            m_delegateStore[clickEventKey] = (EventHandler)
                Delegate.Combine(m_delegateStore[clickEventKey], value);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        remove
        {
            m_delegateStore[clickEventKey] = (EventHandler)
                Delegate.Remove(m_delegateStore[clickEventKey], value); 
        }
    }

    protected void OnClick()
    {
        EventHandler handler = m_delegateStore[clickEventKey];
        if (handler != null)
        {
            handler(this, null);
        }
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

        button.Click += ButtonHandler;

        button.SimulateClick();

        button.Click -= ButtonHandler;
    }
}
}
