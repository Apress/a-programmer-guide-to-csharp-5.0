using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
class Employee : ISerializable
{
    int m_id;
    string m_name;
    string m_address;

    public Employee(int id, string name, string address)
    {
        m_id = id;
        m_name = name;
        m_address = address;
    }

    public override string ToString()
    {
        return (String.Format("{0} {1} {2}", m_id, m_name, m_address));
    }

    Employee(SerializationInfo info, StreamingContext content)
    {
        m_id = info.GetInt32("id");
        m_name = info.GetString("name");
        m_address = info.GetString("address");
    }

    // called to save the object data...        
    public void GetObjectData(SerializationInfo info, StreamingContext content)
    {
        info.AddValue("id", m_id);
        info.AddValue("name", m_name);
        info.AddValue("address", m_address);
    }
}

class CustomSerializationTest
{
    public static void Serialize(Employee employee, string filename)
    {
        using (Stream streamWrite = File.Create(filename))
        {
            IFormatter writer = new BinaryFormatter();
            writer.Serialize(streamWrite, employee);
        }
    }

    public static Employee Deserialize(string filename)
    {
        Employee employee = null;
        using (Stream streamRead = File.OpenRead(filename))
        {
            IFormatter reader = new BinaryFormatter();
            employee = (Employee)reader.Deserialize(streamRead);
        }
        return (employee);
    }

    public static void Mainly()
    {
        Employee employee = new Employee(15, "Fred", "Bedrock");

        Serialize(employee, "emp.dat");
        Employee employeeBack = Deserialize("emp.dat");
        Console.WriteLine("Employee: {0}", employeeBack);
    }
}

