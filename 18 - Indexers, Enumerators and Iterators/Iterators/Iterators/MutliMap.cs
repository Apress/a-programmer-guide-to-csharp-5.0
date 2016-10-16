#region Using directives

using System;
using System.Collections;
using System.Text;

#endregion

public class MutliMap
{
 Hashtable map = new Hashtable();

 public void Add(object key, object value)
 {
  if (map[key] == null)
   map[key] = new ArrayList();

  ((ArrayList)map[key]).Add(value);
 }

 public IEnumerator KeysAndValues()
 {
  foreach (object key in map.Keys)
  {
   yield return key;
   if (map[key] != null)
   {
    ArrayList values = (ArrayList)map[key];
    foreach (object value in values)
    {
     yield return value;
    }
   }
  }
 }
}
