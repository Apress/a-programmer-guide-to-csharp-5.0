using System;
using System.Collections.Generic;
using System.Text;

class Coalescing
{
 public Coalescing()
 {
  int? x = null;
  int y = x ?? 2; //y will be 2
  //same as:
  y = x.HasValue ? x.GetValueOrDefault() : 2;

  x = 3;
  y = x ?? 2; //y will be 3
 }
}

