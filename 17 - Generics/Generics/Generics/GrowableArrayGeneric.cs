#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

public class GrowableArray<T>
{
 private T[] _collection = new T[16];
 private int _count = 0;

 public void AddElement(T element)
 {
  if (_count >= _collection.Length)
  {
   T[] temp = new T[_collection.Length * 2];
   Array.Copy(_collection, temp, _collection.Length);
   _collection = temp;
  }
  _collection[_count] = element;
  ++_count;
 }

 public T GetElement(int elementNumber)
 {
  if (elementNumber >= _count)
  {
   throw new IndexOutOfRangeException();
  }
  return _collection[elementNumber];
 }
}
