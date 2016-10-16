namespace other
{

 using System;
 using System.Collections.Generic;


 public class Employee : IComparable<Employee>
 {
  public Employee(string name, int id)
  {
   this.name = name;
   this.id = id;
  }

  int IComparable<Employee>.CompareTo(Employee emp2)
  {
   if (this.id > emp2.id)
    return (1);
   if (this.id < emp2.id)
    return (-1);
   else
    return (0);
  }

  bool IComparable<Employee>.Equals(Employee emp2)
  {
   if (emp2 == null)
    return false;

   return id == emp2.id && name == emp2.name;
  }

  public override string ToString()
  {
   return (name + ":" + id);
  }
  public class SortByNameClass : IComparer<Employee>
  {
   public int Compare(Employee emp1, Employee emp2)
   {
    return (String.Compare(emp1.name, emp2.name));
   }
   public bool Equals(Employee emp1, Employee emp2)
   {
    return Compare(emp1, emp2) == 0;
   }
   public int GetHashCode(Employee emp)
   {
    return emp.name.GetHashCode();
   }
  }
  public class SortByIdClass : IComparer<Employee>
  {
   public int Compare(Employee emp1, Employee emp2)
   {
    return (((IComparable<Employee>)emp1).CompareTo(emp2));
   }
   public bool Equals(Employee emp1, Employee emp2)
   {
    return Compare(emp1, emp2) == 0;
   }
   public int GetHashCode(Employee emp)
   {
    return emp.id.GetHashCode();
   }
  }
  string name;
  int id;
 }
 class Test2
 {
  public static void Main()
  {
   Employee[] arr = new Employee[4];
   arr[0] = new Employee("George", 1);
   arr[1] = new Employee("Fred", 2);
   arr[2] = new Employee("Tom", 4);
   arr[3] = new Employee("Bob", 3);
   Array.Sort<Employee>(arr, (IComparer<Employee>)new Employee.SortByNameClass());
   // employees is now sorted by name
   foreach (Employee emp in arr)
    Console.WriteLine("Employee: {0}", emp);
   Array.Sort<Employee>(arr, (IComparer<Employee>)new Employee.SortByIdClass());
   // employees is now sorted by id
   foreach (Employee emp in arr)
    Console.WriteLine("Employee: {0}", emp);
   List<Employee> list = new List<Employee>();
   list.Add(arr[0]);
   list.Add(arr[1]);
   list.Add(arr[2]);
   list.Add(arr[3]);
   list.Sort((IComparer<Employee>)new Employee.SortByNameClass());
   foreach (Employee emp in list)
    Console.WriteLine("Employee: {0}", emp);
   list.Sort(); // default is by id
   foreach (Employee emp in list)
    Console.WriteLine("Employee: {0}", emp);
  }
 }
}