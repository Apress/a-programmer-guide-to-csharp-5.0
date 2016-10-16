// 32 - .NET Frameworks Overview\Serialization
// copyright 2000 Eric Gunnerson
// file=serial.cs
// compile with: csc serial.cs
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

[Serializable]
public class MyElement
{
    public MyElement(string name)
    {
        this.name = name;
        this.cacheValue = 15;
    }
    public override string ToString()
    {
        return(String.Format("{0}: {1}", name, cacheValue));
    }
    string name;
    // this field isn't persisted.
    [NonSerialized]
    int cacheValue;
}
[Serializable]
public class MyRow
{
    public void Add(MyElement my)
    {
        row.Add(my);
    }
    
    public override string ToString()
    {
        string temp = null;
        foreach (MyElement my in row)
        temp += my.ToString() + "\n"; 
        return(temp);
    }
    
    ArrayList row = new ArrayList();    
}

class Test
{
    public static void Main()
    {
        MyRow row = new MyRow();
        row.Add(new MyElement("Gumby"));
        row.Add(new MyElement("Pokey"));
        
        Console.WriteLine("Initial value");
        Console.WriteLine("{0}", row);
        
        // write to binary, read it back
        Stream streamWrite = File.Create("MyRow.bin");
        BinaryFormatter binaryWrite = new BinaryFormatter();
        binaryWrite.Serialize(streamWrite, row);
        streamWrite.Close();
        
        Stream streamRead = File.OpenRead("MyRow.bin");
        BinaryFormatter binaryRead = new BinaryFormatter();
        MyRow rowBinary = (MyRow) binaryRead.Deserialize(streamRead);
        streamRead.Close();
        
        Console.WriteLine("Values after binary serialization");
        Console.WriteLine("{0}", rowBinary);
        
        // write to SOAP (XML), read it back
        streamWrite = File.Create("MyRow.xml");
        SoapFormatter soapWrite = new SoapFormatter();
        soapWrite.Serialize(streamWrite, row);
        streamWrite.Close();
        
        streamRead = File.OpenRead("MyRow.xml");
        SoapFormatter soapRead = new SoapFormatter();
        MyRow rowSoap = (MyRow) soapRead.Deserialize(streamRead);
        streamRead.Close();
        
        Console.WriteLine("Values after SOAP serialization");
        Console.WriteLine("{0}", rowSoap);
    }
}