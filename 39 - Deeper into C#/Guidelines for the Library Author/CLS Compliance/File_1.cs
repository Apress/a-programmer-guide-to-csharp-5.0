// 36 - Deeper into C#\Guidelines for the Library Author\CLS Compliance
// copyright 2000 Eric Gunnerson
// error
using System;

[CLSCompliant(true)]

class Test
{
public uint Process() {return(0);}
}