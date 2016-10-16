// 06 - Base Classes and Inheritance\Virtual Functions
// copyright 2000 Eric Gunnerson
using System;
class Engineer
{
    public Engineer(string name, float billingRate)
    {
        this.name = name;
        this.billingRate = billingRate;
    }
    // function now virtual
    virtual public float CalculateCharge(float hours)
    {
        return(hours * billingRate);
    }
    // function now virtual
    virtual public string TypeName()
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
    // overrides function in Engineer
    override public float CalculateCharge(float hours)
    {
        if (hours < 1.0F)
        hours = 1.0F;        // minimum charge.
        return(hours * billingRate);
    }
    // overrides function in Engineer
    override public string TypeName()
    {
        return("Civil Engineer");
    }
}
class Test
{
    public static void Main()
    {
        Engineer[]    earray = new Engineer[2];
        earray[0] = new Engineer("George", 15.50F);
        earray[1] = new CivilEngineer("Sir John", 40F);
        
        Console.WriteLine("{0} charge = {1}",
        earray[0].TypeName(),
        earray[0].CalculateCharge(2F));
        Console.WriteLine("{0} charge = {1}",
        earray[1].TypeName(),
        earray[1].CalculateCharge(0.75F));
    }
}