// 19 - Indexers and Enumerators\Indexing with an Integer Index
// copyright 2000 Eric Gunnerson
using System;
using System.Collections;
class DataValue
{
    public DataValue(string name, object data)
    {
        this.name = name;
        this.data = data;
    }
    public string Name
    {
        get
        {
            return(name);
        }
        set
        {
            name = value;
        }
    }
    public object Data
    {
        get
        {
            return(data);
        }
        set
        {
            data = value;
        }
    }
    string    name;
    object data;
}
class DataRow
{
    public DataRow()
    {
        row = new ArrayList();
    }
    
    public void Load() 
    {
        /* load code here */ 
        row.Add(new DataValue("Id", 5551212));
        row.Add(new DataValue("Name", "Fred"));
        row.Add(new DataValue("Salary", 2355.23m));
    }
    
    // the indexer
    public DataValue this[int column]
    {
        get
        {
            return((DataValue) row[column - 1]);
        }
        set
        {
            row[column - 1] = value;
        }
    }
    ArrayList    row;    
}
class Test
{
    public static void Main()
    {
        DataRow row = new DataRow();
        row.Load();
        Console.WriteLine("Column 0: {0}", row[1].Data);
        row[1].Data = 12;    // set the ID
    }
}