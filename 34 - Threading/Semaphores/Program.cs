using System;
using System.Threading;
using System.Text;

namespace Threads
{
 class Program
 {
  static void Main(string[] args)
  {
  }

  static void Mutex()
  {
   //create a Mutex called "PORT_1234_PROTECT".  The mutex is not initially
   //owned by this thread
   Mutex portProtector = new Mutex(false, "PORT_1234_PROTECT");
   try
   {
    portProtector.WaitOne();
    //operations on port that need to be protected
   }
   finally
   {
    portProtector.ReleaseMutex();
   }
  }

  static void Semaphore()
  {
   //create a Semaphore called "PORT_12xx_PROTECT".  The thread has an initial count
   //of 0 on the semaphore.  The semaphore has a maximum count of 6
   Semaphore mutiplePortProtector = new Semaphore(0, 6, "PORT_12xx_PROTECT");
   try
   {
    mutiplePortProtector.WaitOne();
    //operations on port that need to be protected
   }
   finally
   {
    mutiplePortProtector.Release();
   }
  }
 }
}
