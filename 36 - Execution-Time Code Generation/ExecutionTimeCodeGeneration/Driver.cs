namespace Polynomial
{
 using System;
 using PolyInterface;
 using System.Diagnostics;
 /// <summary>
 /// Driver class for the project
 /// </summary>
 public class Driver
 {
  /// <summary>
  /// Times the evaluation of a polynomial
  /// </summary>
  /// <param name="p">The polynomial to evaluate</param>
  public static double TimeEvaluate(Polynomial p)
  {
   double value = 2.0;
   Console.WriteLine("{0}", p.GetType().Name);
   // Time the first iteration. This one is done
   // separately so that we can figure out the startup
   // overhead separately...
   long start = Stopwatch.GetTimeStamp();
   IPolynomial iPoly = p.GetEvaluate();
   long delta = Stopwatch.GetTimeStamp() -start;
   Console.WriteLine("Overhead = {0:f2} seconds",
   (double)delta / Stopwatch.Frequency);
   Console.WriteLine("Eval({0}) = {1}", value, iPoly.Evaluate(value));
   int limit = 100000;
   Stopwatch timer = Stopwatch.StartNew();
   // Evaluate the polynomial the required number of
   // times.
   double result = 0;
   for (int i = 0; i < limit; i++)
   {
    result += iPoly.Evaluate(value);
   }
   timer.Stop();
   double ips = (double)limit / (double)timer.ElapsedMilliseconds / 1000D;
   Console.WriteLine("Evalutions/Second = {0:f0}", ips);
   Console.WriteLine();
   return (ips);
  }
  /// <summary>
  /// Run all implementations for a given set of coefficients
  /// </summary>
  /// <param name="coeff"> </param>
  public static void Eval(double[] coeff)
  {
   Polynomial[] imps = new Polynomial[] { new PolyEmit(coeff) , new PolyCodeDom(coeff)};
   double[] results = new double[imps.Length];
   for (int index = 0; index < imps.Length; index++)
   {
    results[index] = TimeEvaluate(imps[index]);
   }
   Console.WriteLine("Results for length = {0}", coeff.Length);
   for (int index = 0; index < imps.Length; index++)
   {
    Console.WriteLine("{0} = {1:f0}", imps[index], results[index]);
   }
   Console.WriteLine();
  }
  /// <summary>
  /// Maim function.
  /// </summary>
  public static void Main()
  {
   (new LightWeightPoly()).Eval();

   Eval(new Double[] { 5.5 });
   // Evaluate the first polynomial, with 7 elements
   double[] coeff = new double[] { 5.5, 7.0, 15, 30, 500, 100, 1 };
   Eval(coeff);
   // Evaluate the second polynomial, with 50 elements
   coeff = new double[50];
   for (int index = 0; index < 50; index++)
   {
    coeff[index] = index;
   }
   Eval(coeff);
  }
 }
}