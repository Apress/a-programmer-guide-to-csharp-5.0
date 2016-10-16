// 28 - System.Array and the Collection Classes\Sorting and Searching\Using IComparer
// copyright 2000 Eric Gunnerson
using System;
using System.Collections;
class Employee
{
    public string name;
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