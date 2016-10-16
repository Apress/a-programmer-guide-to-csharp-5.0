#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#endregion


// Note: This class is not thread-safe
public class IntList : IEnumerable
{
 int[] values = new int[10];
 int allocated = 10;
 int count = 0;
 int revision = 0;
 public void Add(int value)
 {
// reallocate if necessary...
  if (count + 1 == allocated)
  {
   int[] newValues = new int[allocated * 2];
   for (int index = 0; index < count; index++)
   {
    newValues[index] = values[index];
   }
   allocated *= 2;
  }
  values[count] = value;
  count++;
  revision++;
 }
 public int Count
 {
  get
  {
   return (count);
  }
 }
 void CheckIndex(int index)
 {
  if (index >= count)
   throw new ArgumentOutOfRangeException("Index value out of range");
 }
 public int this[int index]
 {
  get
  {
   CheckIndex(index);
   return (values[index]);
  }
  set
  {
   CheckIndex(index);
   values[index] = value;
   revision++;
  }
 }
 public IEnumerator GetEnumerator()
 {
  for (int index = 0; index < count; ++index)
  {
   yield return values[index];
  }
 }

 public IEnumerable BidirectionalSubrange(bool forward, int start, int end)
 {
  if (start < 0 || end >= count)
  {
   throw new IndexOutOfRangeException("Start must be zero or greater and" +
    " end must be less than the size of the collection");
  }

  int step = forward == true ? 1 : -1;
  for (int index = start; index != end; index += step)
  {
   yield return values[index];
  }
 }

 internal int Revision
 {
  get
  {
   return (revision);
  }
 }
}
