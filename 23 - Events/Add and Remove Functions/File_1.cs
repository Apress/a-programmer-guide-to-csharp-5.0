// 23 - Events\Add and Remove Functions
// copyright 2000 Eric Gunnerson
using System;

public class Button
{
    public delegate void ClickHandler(object sender, EventArgs e);
    private ClickHandler click;
    
    public void AddClick(ClickHandler clickHandler)
    {
        click += clickHandler;
    }
    
    public void RemoveClick(ClickHandler clickHandler)
    {
        click -= clickHandler;
    }
    
    protected void OnClick()
    {
        if (click != null)
        click(this, null);
        
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
        
        button.AddClick(new Button.ClickHandler(ButtonHandler));
        
        button.SimulateClick();
        
        button.RemoveClick(new Button.ClickHandler(ButtonHandler));
    }
}