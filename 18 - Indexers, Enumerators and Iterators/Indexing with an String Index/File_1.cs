// 19 - Indexers and Enumerators\Indexing with an String Index
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
    
    public DataValue this[int column]
    {
        get
        {
            return( (DataValue) row[column - 1]);
        }
        set
        {
            row[column - 1] = value;
        }
    }
    int FindColumn(string name)
    {
        for (int index = 0; index < row.Count; index++)
        {
            DataValue dataValue = (DataValue) row[index];
            if (dataValue.Name == name)
            return(index);
        }
        return(-1);
    }
    public DataValue this[string name]
    {
        get
        {
            return( (DataValue) this[FindColumn(name)]);
        }
        set
        {
            this[FindColumn(name)] = value;
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
        DataValue val = row["Id"];
        Console.WriteLine("Id: {0}", val.Data);
        Console.WriteLine("Salary: {0}", row["Salary"].Data);
        row["Name"].Data = "Barney";    // set the name
        Console.WriteLine("Name: {0}", row["Name"].Data);
    }
}