using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolynomialBase;

namespace PolynomialImplementation
{
    using System;
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            IPolynomial iPoly = p.GetEvaluate();
            stopwatch.Stop();

            Console.WriteLine("Overhead = {0:f2} seconds", stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Eval({0}) = {1}", value, iPoly.Evaluate(value));

            int limit = 1000000;
            stopwatch.Reset();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            stopwatch.Start();

            // Evaluate the polynomial the required number of
            // times.
            double result = 0;
            for (int i = 0; i < limit; i++)
            {
                result += iPoly.Evaluate(value);
            }
            stopwatch.Stop();
            Console.WriteLine("Result: {0}", result);
            Console.WriteLine("Time: {0}", stopwatch.Elapsed);

            double ips = (double)limit / stopwatch.Elapsed.TotalSeconds;
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
            Polynomial[] imps = new Polynomial[]
            {
                    new PolyCodeSlow(coeff),
                    new PolySimple(coeff),
                    new PolyCodeFast(coeff),
                    new PolyBrokenFastest(coeff),
                 
            };
            for (int i = 0; i < imps.Length; i++)
            {
                imps[i].GetEvaluate().Evaluate(3);
            }


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

