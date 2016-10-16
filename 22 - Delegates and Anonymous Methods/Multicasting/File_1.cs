// 22 - Delegates\Multicasting
// copyright 2000 Eric Gunnerson
using System;
public class User
{
    string name;
    public User(string name)
    {
        this.name = name;
    }    
    public void Process(string message)
    {
        Console.WriteLine("{0}: {1}", name, message);
    }
}

class Test
{
    delegate void ProcessHandler(string message);
    
    static public void Process(string message)
    {
        Console.WriteLine("Test.Process(\"{0}\")", message);
    }
    public static void Main()
    {
        User user = new User("George");
        
        ProcessHandler ph = new ProcessHandler(user.Process);
        ph = (ProcessHandler) Delegate.Combine(ph, new ProcessHandler(Process));
        
        ph("Wake Up!");        
    }
}