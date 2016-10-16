// 23 - Events
// copyright 2000 Eric Gunnerson
using System;

public class Button
{
    public delegate void ClickHandler(object sender, EventArgs e);
    public ClickHandler Click;
    
    protected void OnClick()
    {
        if (Click != null)
        Click(this, null);
        
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
        
        button.Click = new Button.ClickHandler(ButtonHandler);
        
        button.SimulateClick();
    }
}