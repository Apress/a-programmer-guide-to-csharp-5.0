// 06 - Base Classes and Inheritance\Arrays of Engineers
// copyright 2000 Eric Gunnerson
using System;
enum EngineerTypeEnum
{
    Engineer,
    CivilEngineer
}
class Engineer
{
    public Engineer(string name, float billingRate)
    {
        this.name = name;
        this.billingRate = billingRate;
        type = EngineerTypeEnum.Engineer;
    }
    
    public float CalculateCharge(float hours)
    {
        if (type == EngineerTypeEnum.CivilEngineer)
        {
            CivilEngineer c = (CivilEngineer) this;
            return(c.CalculateCharge(hours));
        }
        else if (type == EngineerTypeEnum.Engineer)    
        return(hours * billingRate);
        return(0F);
    }
    
    public string TypeName()
    {
        if (type == EngineerTypeEnum.CivilEngineer)
        {
            CivilEngineer c = (CivilEngineer) this;
            return(c.TypeName());
        }
        else if (type == EngineerTypeEnum.Engineer)
        return("Engineer");
        return("No Type Matched");
    }
    
    private string name;
    protected float billingRate;
    protected EngineerTypeEnum type;
}

class CivilEngineer: Engineer
{
    public CivilEngineer(string name, float billingRate) :
    base(name, billingRate)
    {
        type = EngineerTypeEnum.CivilEngineer;
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