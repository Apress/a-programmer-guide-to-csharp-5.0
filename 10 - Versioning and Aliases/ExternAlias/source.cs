extern alias Maths;
extern alias Utils;

namespace AcmeScientific.MyApp
{
class App{
  static void Main()
  {
   Maths::AcmeScientific.Math m = new Maths::AcmeScientific.Math();
   m.Calc();

   Utils::AcmeScientific.Math u = new Utils::AcmeScientific.Math();
   u.DoSums();
  }
 }
}