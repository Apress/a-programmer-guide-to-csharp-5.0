// 16 - Arrays\Arrays of Reference Types
// copyright 2000 Eric Gunnerson
class Employee
{
    public static Employee LoadFromDatabase(int employeeID)
    {
        Employee emp = new Employee();
        // load code here
        return(emp);
    }
}
class Test
{
    public static void Main()
    {
        Employee[] emps = new Employee[3];
        emps[0] = Employee.LoadFromDatabase(15);
        emps[1] = Employee.LoadFromDatabase(35);
        emps[2] = Employee.LoadFromDatabase(255);
    }
}