// 37 - Defensive Programming\Using Switches to Control Debug and Trace\User-Defined Switch
// copyright 2000 Eric Gunnerson
// compile with: csc /r:system.dll file_1.cs
using System;
using System.Diagnostics;

enum SpecialSwitchLevel
{
    Mute = 0,
    Terse = 1,
    Verbose = 2,
    Chatty = 3
}

class SpecialSwitch: Switch
{
    public SpecialSwitch(string displayName, string description) :
    base(displayName, description)
    {
    }
    
    public SpecialSwitchLevel Level
    {
        get
        {
            return((SpecialSwitchLevel) base.SwitchSetting);
        }
        set
        {
            base.SwitchSetting = (int) value;
        }    
    }
    public bool Mute
    {
        get
        {
            return(base.SwitchSetting == 0);
        }
    }
    
    public bool Terse
    {
        get
        {
            return(base.SwitchSetting >= (int) (SpecialSwitchLevel.Terse));
        }
    }
    public bool Verbose
    {
        get
        {
            return(base.SwitchSetting >= (int) SpecialSwitchLevel.Verbose);
        }
    }
    public bool Chatty
    {
        get
        {
            return(base.SwitchSetting >=(int) SpecialSwitchLevel.Chatty);
        }
    }
    
    protected new int SwitchSetting
    {
        get
        {
            return((int) base.SwitchSetting);
        }
        set
        {
            if (value < 0)
            value = 0;
            if (value > 4)
            value = 4;
            
            base.SwitchSetting = value;
        }
    }
}

class MyClass
{
    public MyClass(int i)
    {
        this.i = i;
    }
    
    [Conditional("DEBUG")]
    public void VerifyState()
    {
        Console.WriteLine("VerifyState");
        Debug.WriteLineIf(debugOutput.Terse, "VerifyState Start");
        
        Debug.WriteLineIf(debugOutput.Chatty, 
        "Starting field verification");
        
        if (debugOutput.Verbose)
        Debug.WriteLine("VerifyState End");
    }
    
    static SpecialSwitch    debugOutput = 
    new SpecialSwitch("MyClassDebugOutput", "application");
    int i = 0;
}

class Test
{
    public static void Main()
    {
        //TraceSwitch ts = new TraceSwitch("MyClassDebugOutput", "application");
        //Console.WriteLine("TraceSwitch: {0}", ts.Level);
        
        Debug.Listeners.Clear();
        Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
        MyClass c = new MyClass(1);
        
        c.VerifyState();
    }
}