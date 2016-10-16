using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

[Serializable]
public class MyElement
{
    public MyElement(string name)
    {
        m_name = name;
        m_cacheValue = 15;
    }
    public override string ToString()
    {
        return String.Format("{0}: {1}", m_name, m_cacheValue);
    }
    string m_name;
    // this field isn't persisted.
    [NonSerialized]
    int m_cacheValue;
}
[Serializable]
public class MyRow
{
    public void Add(MyElement myElement)
    {
        m_elements.Add(myElement);
    }
    public override string ToString()
    {
        return String.Join(
            "\n",
            m_elements
                .Select(element => element.ToString())
                .ToList());
    }
    List<MyElement> m_elements = new List<MyElement>();
}

public class SerializationTest
{
    public static void Mainly()
    {
        MyRow row = new MyRow();
        row.Add(new MyElement("Gumby"));
        row.Add(new MyElement("Pokey"));

        Console.WriteLine("Initial value");
        Console.WriteLine("{0}", row);

        // write to binary, read it back
        using (Stream streamWrite = File.Create("MyRow.bin"))
        {
            BinaryFormatter binaryWrite = new BinaryFormatter();
            binaryWrite.Serialize(streamWrite, row);
        }

        MyRow rowBinary = null;
        using (Stream streamRead = File.OpenRead("MyRow.bin"))
        {
            BinaryFormatter binaryRead = new BinaryFormatter();
            rowBinary = (MyRow)binaryRead.Deserialize(streamRead);
        }

        Console.WriteLine("Values after binary serialization");
        Console.WriteLine("{0}", rowBinary);
    }
}

