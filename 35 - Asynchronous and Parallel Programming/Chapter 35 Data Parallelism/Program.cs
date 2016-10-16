using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter_35_Data_Parallelism
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateNumbersToSum();
            SumOfSums();
            SumOfSumsParallelWithResult();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            long result1 = SumOfSums();
            stopwatch.Stop();
            double time1 = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine(stopwatch.Elapsed);
            stopwatch.Reset();

            stopwatch.Start();
            //long result2 = SumOfSumsParallelWithConcurrentBag();
            long result2 = SumOfSumsPLinq();
            stopwatch.Stop();
            double time2 = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine(stopwatch.Elapsed);

            Console.WriteLine("Ratio: {0}", time1 / time2);
            if (result1 != result2)
            {
                Console.WriteLine("{0}, {1}", result1, result2);
            }
        }

        static List<int> m_numbersToSum = new List<int>();

        static void GenerateNumbersToSum()
        {
            for (int i = 0; i < 50000; i++)
            {
                m_numbersToSum.Add(i);
            }
        }

static long SumOfSums()
{
    long sumOfSums = 0;

    foreach (int number in m_numbersToSum)
    {
        sumOfSums += SumNumbersLessThan(number);
    }
    return sumOfSums;
}

static void SumOfSumsParallel()
{
    Parallel.ForEach(m_numbersToSum, number =>
        {
            SumNumbersLessThan(number);
        });
}

static long m_sumOfSums;

static long SumOfSumsParallelWithResult()
{
    m_sumOfSums = 0;

    Parallel.ForEach(m_numbersToSum, number =>
    {
        long sum = SumNumbersLessThan(number);
        Interlocked.Add(ref m_sumOfSums, sum);
    });

    return m_sumOfSums;
}

static ConcurrentBag<long> m_results = new ConcurrentBag<long>();

static long SumOfSumsParallelWithConcurrentBag()
{
    Parallel.ForEach(m_numbersToSum, number =>
    {
        long sum = SumNumbersLessThan(number);
        m_results.Add(sum);
    });

    return m_results.Sum();
}

static long SumOfSumsPLinq()
{
    return
        m_numbersToSum
            .AsParallel()
            .Select(number => SumNumbersLessThan(number))
            .Sum();
}

static long SumNumbersLessThan(int limit)
{
    long sum = 0;
    for (int i = 0; i < limit; i++)
    {
        sum += 1;
    }

    return sum;
}
    }
}
