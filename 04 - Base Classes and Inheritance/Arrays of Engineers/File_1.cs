// 06 - Base Classes and Inheritance\Arrays of Engineers
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
    
    public new float CalculateCharge(float hours)
    {
        if (hours < 1.0F)
        hours = 1.0F;        // minimum charge.
        return(hours * billingRate);
    }
    
    public new string TypeName()
    {
        return("Civil Engineer");
    }
}
class Test
{
    public static void Main()
    {
        // create an array of engineers
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