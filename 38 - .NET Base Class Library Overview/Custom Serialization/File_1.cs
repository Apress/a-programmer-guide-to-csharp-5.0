// 32 - .NET Frameworks Overview\Custom Serialization
// copyright 2000 Eric Gunnerson
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

class Employee: ISerializable
{
    int id;
    string name;
    string address;
    
    public Employee(int id, string name, string address)
    {
        this.id = id;
        this.name = name;
        this.address = address;
    }
    
    public override string ToString()
    {
        return(String.Format("{0} {1} {2}", id, name, address));
    }
    
    Employee(SerializationInfo info, StreamingContext content)
    {
        id = info.GetInt32("id");
        name = info.GetString("name");
        address = info.GetString("address");
    }
    
    // called to save the object data        
    public void GetObjectData(SerializationInfo info, StreamingContext content)
    {
        info.AddValue("id", id);
        info.AddValue("name", name);
        info.AddValue("address", address);
    }
}

class Test
{
    public static void Serialize(Employee employee, string filename)
    {
        Stream streamWrite = File.Create(filename);
        IFormatter writer = new SoapFormatter();
        writer.Serialize(streamWrite, employee);
        streamWrite.Close();
    }
    
    public static Employee Deserialize(string filename)
    {
        Stream streamRead = File.OpenRead(filename);
        IFormatter reader = new SoapFormatter();
        Employee employee = (Employee) reader.Deserialize(streamRead);
        streamRead.Close();
        return(employee);
    }
    
    public static void Main()
    {
        Employee employee = new Employee(15, "Fred", "Bedrock");
        
        Serialize(employee, "emp.dat");
        employee = Deserialize("emp.dat");
        Console.WriteLine("Employee: {0}", employee);
    }
}