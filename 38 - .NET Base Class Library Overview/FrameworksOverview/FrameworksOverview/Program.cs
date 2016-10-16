using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworksOverview
{
 class Program
 {
  static void Main(string[] args)
  {
   Console.WriteLine("Please enter an integer and press Enter");
   int numberEntered;
   while(!int.TryParse(Console.ReadLine(), out numberEntered)){
    Console.WriteLine("Please try again");
   }
   Console.WriteLine("You entered " + numberEntered.ToString());
  }
 }
}
