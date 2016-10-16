#region Using directives

using System;
using System.Collections;
using System.Text;

#endregion

public class Person
{
 string firstName;
 string middleName;
 string lastName;

 public Person(string firstName, string middleName, string lastName)
 {
  this.firstName = firstName;
  this.middleName = middleName;
  this.lastName = lastName;
 }

 public IEnumerable Names()
 {
  yield return firstName;
  yield return middleName;
  yield return lastName;
 }
}

