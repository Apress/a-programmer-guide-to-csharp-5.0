// 06 - Base Classes and Inheritance\Abstract Classes
// copyright 2000 Eric Gunnerson
using System;
abstract class Engineer
{
    public Engineer(string name, float billingRate)
    {
        this.name = name;
        this.billingRate = billingRate;
    }
    
    virtual public float CalculateCharge(float hours)
    {
        return(hours * billingRate);
    }
    
    abstract public string TypeName();
    
    private string name;
    protected float billingRate;
}

class CivilEngineer: Engineer
{
    public CivilEngineer(string name, float billingRate) :
    base(name, billingRate)
    {
    }
    
    override public float CalculateCharge(float hours)
    {
        if (hours < 1.0F)
        hours = 1.0F;        // minimum charge.
        return(hours * billingRate);
    }
    
    // This override is required, or an error is generated.
    override public string TypeName()
    {
        return("Civil Engineer");
    }
}

class ChemicalEngineer: Engineer
{
    public ChemicalEngineer(string name, float billingRate) :
    base(name, billingRate)
    {
    }
    
    override public string TypeName()
    {
        return("Chemical Engineer");
    }
}
class Test
{
    public static void Main()
    {
        Engineer[]    earray = new Engineer[2];
        earray[0] = new CivilEngineer("Sir John", 40.0F);
        earray[1] = new ChemicalEngineer("Dr. Curie", 45.0F);
        
        Console.WriteLine("{0} charge = {1}",
        earray[0].TypeName(),
        earray[0].CalculateCharge(2F));
        Console.WriteLine("{0} charge = {1}",
        earray[1].TypeName(),
        earray[1].CalculateCharge(0.75F));
    }
}