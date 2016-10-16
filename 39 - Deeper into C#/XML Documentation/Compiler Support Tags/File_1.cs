// 36 - Deeper into C#\XML Documentation\Compiler Support Tags
// copyright 2000 Eric Gunnerson
// file: employee.cs
using System;
namespace Payroll
{
    
    /// <summary> 
    /// The Employee class holds data about an employee.
    /// This class class contains a <see cref="String">string</see>
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Constructor for an Employee instance. Note that
        /// <paramref name="name">name2</paramref> is a string.
        /// </summary>
        /// <param name="id">Employee id number</param>
        /// <param name="name">Employee Name</param>
        public Employee(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        
        /// <summary>
        /// Parameterless constructor for an employee instance
        /// </summary>
        /// <remarks>
        /// <seealso cref="Employee(int, string)">Employee(int, string)</seealso>
        /// </remarks>
        public Employee()
        {
            id = -1;
            name = null;
        }
        int id;
        string name;
    }
}