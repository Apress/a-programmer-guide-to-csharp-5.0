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
  return (String.Format("{0}:{1}", name, id));
 }
 string name;
 int id;
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
  Array.Sort(arr);
  foreach (Employee emp in arr)
   Console.WriteLine("Employee: {0}", emp);
  // Find employee id 2 in the list;
  Employee employeeToFind = new Employee(null, 2);
  int index = Array.BinarySearch(arr, employeeToFind);
  if (index != -1)
   Console.WriteLine("Found: {0}", arr[index]);
 }
}