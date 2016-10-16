// 28 - System.Array and the Collection Classes\Sorting and Searching\Overloading Relational Operators
// copyright 2000 Eric Gunnerson
using System;
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
    public static bool operator <(
    Employee emp1,
    Employee emp2)
    {
        IComparable    icomp = (IComparable) emp1;
        return(icomp.CompareTo (emp2) < 0);
    }
    public static bool operator >(
    Employee emp1,
    Employee emp2)
    {
        IComparable    icomp = (IComparable) emp1;
        return(icomp.CompareTo (emp2) > 0);
    }
    public static bool operator <=(
    Employee emp1,
    Employee emp2)
    {
        IComparable    icomp = (IComparable) emp1;
        return(icomp.CompareTo (emp2) <= 0);
    }
    public static bool operator >=(
    Employee emp1,
    Employee emp2)
    {
        IComparable    icomp = (IComparable) emp1;
        return(icomp.CompareTo (emp2) >= 0);
    }
    
    public override string ToString()
    {
        return(name + ":" + id);
    }
    
    string    name;
    int    id;
}
class Test
{
    public static void Main()
    {
        Employee george = new Employee("George", 1);
        Employee fred = new Employee("Fred", 2);
        Employee tom = new Employee("Tom", 4);
        Employee bob = new Employee("Bob", 3);
        
        Console.WriteLine("George < Fred: {0}", george < fred);
        Console.WriteLine("Tom >= Bob: {0}", tom >= bob);
    }
} 