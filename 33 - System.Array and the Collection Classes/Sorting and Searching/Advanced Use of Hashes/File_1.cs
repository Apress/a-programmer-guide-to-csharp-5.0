// 28 - System.Array and the Collection Classes\Sorting and Searching\Advanced Use of Hashes
// copyright 2000 Eric Gunnerson
using System;
using System.Collections;

public class Employee: IComparable
{
    public Employee(string name, int id)
    {
        this.name = name;
        this.id = id;
    }
    
    int IComparable.CompareTo(object obj)
    {
        Employee emp2 = (Employee) obj;
        if (this.id > emp2.id)
        return(1);
        if (this.id < emp2.id)
        return(-1);
        else
        return(0);
    }
    public override int GetHashCode()
    {
        return(id);
    }
    public static IComparer SortByName
    {
        get
        {
            return((IComparer) new SortByNameClass());
        }
    }
    
    public static IComparer SortById
    {
        get
        {
            return((IComparer) new SortByIdClass());
        }
    }
    public static IHashCodeProvider HashByName
    {
        get
        {
            return((IHashCodeProvider) new HashByNameClass());
        }
    }
    public override string ToString()
    {
        return(name + ":" + id);
    }
    
    class SortByNameClass: IComparer
    {
        public int Compare(object obj1, object obj2)
        {
            Employee emp1 = (Employee) obj1;
            Employee emp2 = (Employee) obj2;
            
            return(String.Compare(emp1.name, emp2.name));
        }
    }
    
    class SortByIdClass: IComparer
    {
        public int Compare(object obj1, object obj2)
        {
            Employee emp1 = (Employee) obj1;
            Employee emp2 = (Employee) obj2;
            
            return(((IComparable) emp1).CompareTo(obj2));
        }
    }
    class HashByNameClass: IHashCodeProvider
    {
        public int GetHashCode(object obj)
        {
            Employee emp = (Employee) obj;
            return(emp.name.GetHashCode());
        }
    }
    
    string    name;
    int    id;
}
class Test
{
    public static void Main()
    {
        Employee herb = new Employee("Herb", 555);
        Employee george = new Employee("George", 123);
        Employee frank = new Employee("Frank", 111);
        Hashtable employees = 
        new Hashtable(Employee.HashByName, Employee.SortByName);
        employees.Add(herb, "414 Evergreen Terrace");
        employees.Add(george, "2335 Elm Street");
        employees.Add(frank, "18 Pine Bluff Road");
        Employee herbClone = new Employee("Herb", 000);
        string address =(string) employees[herbClone];
        Console.WriteLine("{0} lives at {1}", herbClone, address);
    }
}