// 06 - Base Classes and Inheritance\Simple Inheritance
// copyright 2000 Eric Gunnerson
using System;
class Engineer
{
    public Engineer(string name, float billingRate)
    {
        this.name = name;
        this.billingRate = billingRate;
    }
    
    public float CalculateCharge(float hours)
    {
        return(hours * billingRate);
    }
    
    public string TypeName()
    {
        return("Engineer");
    }
    
    private string name;
    protected float billingRate;
}
class CivilEngineer: Engineer
{
    public CivilEngineer(string name, float billingRate) :
    base(name, billingRate)
    {
    }
    // new function, because it's different than the
    // base version
    public new float CalculateCharge(float hours)
    {
        if (hours < 1.0F)
        hours = 1.0F;        // minimum charge.
        return(hours * billingRate);
    }
    // new function, because it's different than the
    // base version
    public new string TypeName()
    {
        return("Civil Engineer");
    }
}
class Test
{
    public static void Main()
    {
        Engineer    e = new Engineer("George", 15.50F);
        CivilEngineer    c = new CivilEngineer("Sir John", 40F);
        
        Console.WriteLine("{0} charge = {1}",
        e.TypeName(),
        e.CalculateCharge(2F));
        Console.WriteLine("{0} charge = {1}",
        c.TypeName(),
        c.CalculateCharge(0.75F));
    }
}