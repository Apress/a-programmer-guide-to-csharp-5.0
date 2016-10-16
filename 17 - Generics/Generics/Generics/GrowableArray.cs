#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

public class GrowableArray
{
 private object[] _collection = new object[16];
 private int _count = 0;

 public void AddElement(object element)
 {
  if (_count >= _collection.Length)
  {
   object[] temp = new object[_collection.Length * 2];
   Array.Copy(_collection, temp, _collection.Length);
   _collection = temp;
  }
  _collection[_count] = element;
  ++_count;
 }

 public object GetElement(int elementNumber)
 {
  if (elementNumber >= _count)
  {
   throw new IndexOutOfRangeException();
  }
  return _collection[elementNumber];
 }
}
