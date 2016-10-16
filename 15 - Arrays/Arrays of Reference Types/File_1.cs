// 16 - Arrays\Arrays of Reference Types
// copyright 2000 Eric Gunnerson
class Employee
{
    public void LoadFromDatabase(int employeeID)
    {
        // load code here
    }
}
class Test
{
    public static void Main()
    {
        Employee[] emps = new Employee[3];
        emps[0].LoadFromDatabase(15);
        emps[1].LoadFromDatabase(35);
        emps[2].LoadFromDatabase(255);
    }
}