// 06 - Base Classes and Inheritance\Abstract Classes
// copyright 2000 Eric Gunnerson
using System;
class Engineer
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
    
    virtual public string TypeName()
    {
        return("Engineer");
    }
    
    private string name;
    protected float billingRate;
}
class ChemicalEngineer: Engineer
{
    public ChemicalEngineer(string name, float billingRate) :
    base(name, billingRate)
    {
    }
    
    // overrides mistakenly omitted
}
class Test
{
    public static void Main()
    {
        Engineer[]    earray = new Engineer[2];
        earray[0] = new Engineer("George", 15.50F);
        earray[1] = new ChemicalEngineer("Dr. Curie", 45.50F);
        
        Console.WriteLine("{0} charge = {1}",
        earray[0].TypeName(),
        earray[0].CalculateCharge(2F));
        Console.WriteLine("{0} charge = {1}",
        earray[1].TypeName(),
        earray[1].CalculateCharge(0.75F));
    }
}