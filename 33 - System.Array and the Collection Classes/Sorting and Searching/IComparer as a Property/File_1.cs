// 28 - System.Array and the Collection Classes\Sorting and Searching\IComparer as a Property
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
    
    string    name;
    int    id;
}
class Test
{
    public static void Main()
    {
        Employee[] arr = new Employee[4];
        arr[0] = new Employee("George", 1);
        arr[1] = new Employee("Fred", 2);
        arr[2] = new Employee("Tom", 4);
        arr[3] = new Employee("Bob", 3);
        
        Array.Sort(arr, Employee.SortByName);
        // employees is now sorted by name
        
        foreach (Employee emp in arr)
        Console.WriteLine("Employee: {0}", emp);
        
        Array.Sort(arr, Employee.SortById);
        // employees is now sorted by id
        
        foreach (Employee emp in arr)
        Console.WriteLine("Employee: {0}", emp);
        
        ArrayList arrList = new ArrayList();
        arrList.Add(arr[0]);
        arrList.Add(arr[1]);
        arrList.Add(arr[2]);
        arrList.Add(arr[3]);
        arrList.Sort(Employee.SortByName);
        
        foreach (Employee emp in arrList)
        Console.WriteLine("Employee: {0}", emp);
        
        arrList.Sort();    // default is by id
        
        foreach (Employee emp in arrList)
        Console.WriteLine("Employee: {0}", emp);
    }
}