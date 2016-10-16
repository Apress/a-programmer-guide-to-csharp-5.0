using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_24___Dynamic_types
{
    public class GenericArithmetic
    {
public static T Add<T>(T first, T second)
{
    return (dynamic) first + (dynamic) second;
}

        public static int AddInt(int first, int second)
        {
            return first + second;
        }

public static void Test()
{
    Console.WriteLine(Add(5, 10));
    Console.WriteLine(AddInt(10, 15));
    Console.WriteLine(Add(5.134, 10));
    Console.WriteLine(Add(10, 33.182274));

    Stopwatch stopwatch = new Stopwatch();

    stopwatch.Start();

    int result = 0;
    for (int i = 0; i < 100000000; i++)
    {
        result += Add(5, 10);
    }

    stopwatch.Stop();
    TimeSpan dynamicTime = stopwatch.Elapsed;

    stopwatch.Reset();
    stopwatch.Start();

    result = 0;
    for (int i = 0; i < 100000000; i++)
    {
        result += AddInt(5, 10);
    }

    stopwatch.Stop();
    TimeSpan normalTime = stopwatch.Elapsed;

    Console.WriteLine("Normal: {0}", normalTime.TotalSeconds);
    Console.WriteLine("Dynamic: {0}", dynamicTime.TotalSeconds);
    Console.WriteLine("Ratio: {0}", dynamicTime.TotalSeconds / normalTime.TotalSeconds);


    // time these.

}
    }
}
