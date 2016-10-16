// 22 - Delegates\Delegates as Static Properties
// copyright 2000 Eric Gunnerson
using System;
class Container
{
    public delegate int CompareItemsCallback(object obj1, object obj2);
    public void SortItems(CompareItemsCallback compare)
    {
        // not a real sort, just shows what the
        // inner loop code might do
        int x = 0;
        int y = 1;
        object    item1 = arr[x];
        object item2 = arr[y];
        int order = compare(item1, item2);
    }
    object[]    arr;    // items in the collection
}
class Employee
{
    Employee(string name, int id)
    {
        this.name = name;
        this.id = id;
    }
    public static Container.CompareItemsCallback SortByName
    {
        get
        {
            return(new Container.CompareItemsCallback(CompareName));
        }
    }
    public static Container.CompareItemsCallback SortById
    {
        get
        {
            return(new Container.CompareItemsCallback(CompareId));
        }
    }
    static int CompareName(object obj1, object obj2)
    {
        Employee emp1 = (Employee) obj1;
        Employee emp2 = (Employee) obj2;
        return(String.Compare(emp1.name, emp2.name));
    }
    static int CompareId(object obj1, object obj2)
    {
        Employee emp1 = (Employee) obj1;
        Employee emp2 = (Employee) obj2;
        
        if (emp1.id > emp2.id)
        return(1);
        if (emp1.id < emp2.id)
        return(-1);
        else
        return(0);
    }
    string    name;
    int    id;
}
class Test
{
    public static void Main()
    {
        Container employees = new Container();
        // create and add some employees here
        
        employees.SortItems(Employee.SortByName);
        // employees is now sorted by name
    }
}