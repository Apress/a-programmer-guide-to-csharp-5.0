// 18 - Properties\Use of Properties
// copyright 2000 Eric Gunnerson
using System;
class Auto
{
    public Auto(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
    
    // query to find # produced
    public int ProductionCount
    {
        get
        {
            if (productionCount == -1)
            {
                // fetch count from database here.
            }
            return(productionCount);
        }
    }
    public int SalesCount
    {
        get
        {
            if (salesCount == -1)
            {
                // query each dealership for data
            }
            return(salesCount);
        }
    }
    string name;
    int id;
    int productionCount = -1;
    int salesCount = -1;
}