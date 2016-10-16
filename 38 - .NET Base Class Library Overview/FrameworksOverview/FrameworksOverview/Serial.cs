using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace FrameworksOverview
{
 class Serial
 {
  public Serial()
  {
   byte[] buffer = new byte[256];
   using (SerialPort sp = new SerialPort("COM1", 19200))
   {
    sp.Open();
    //read directly
    sp.Read(buffer, 0, (int)buffer.Length);
    //read using a Stream
    sp.BaseStream.Read(buffer, 0, (int)buffer.Length);
   }
  }
 }
}
