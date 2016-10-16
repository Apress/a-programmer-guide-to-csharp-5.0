using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public struct Point
{
    public Point(int x, int y)
    {
        m_x = x;
        m_y = y;
    }

    // safe version
    public static Point[] ClonePointArray(Point[] sourceArray)
    {
        Point[] result = new Point[sourceArray.Length];

        for (int index = 0; index < sourceArray.Length; index++)
        {
            result[index] = sourceArray[index];
        }

        return (result);
    }

    // unsafe version using pointer arithmetic
    unsafe public static Point[] ClonePointArrayUnsafe(Point[] sourceArray)
    {
        Point[] result = new Point[sourceArray.Length];

        // sourceArray and result are pinned; they cannot be moved by
        // the garbage collector inside the fixed block.
        fixed (Point* src = sourceArray, dest = result)
        {
            Point* pSrc = src;
            Point* pDest = dest;
            for (int index = 0; index < sourceArray.Length; index++)
            {
                *pDest = *pSrc;
                pSrc++;
                pDest++;
            }
        }

        return (result);
    }
    // import CopyMemory from kernel32
    [DllImport("kernel32.dll")]
    unsafe public static extern void
    CopyMemory(void* dest, void* src, int length);

    // unsafe version calling CopyMemory()
    unsafe public static Point[] ClonePointArrayCopyMemory(Point[] sourceArray)
    {
        Point[] result = new Point[sourceArray.Length];

        fixed (Point* src = sourceArray, dest = result)
        {
            CopyMemory(dest, src, sourceArray.Length * sizeof(Point));
        }

        return (result);
    }

    public override string ToString()
    {
        return (String.Format("({0}, {1})", m_x, m_y));
    }

    int m_x;
    int m_y;
}

class Test
{
    const int Iterations = 2000000;    // # to do copy
    const int Points = 10;         // # of points in array
    const int TimeCount = 5;         // # of times to time

    public delegate Point[] CloneFunction(Point[] sourceArray);

    public static void TimeFunction(Point[] sourceArray,
        CloneFunction cloneFunction, string label)
    {
        Point[] result = null;
        TimeSpan minimumElapsedTime = TimeSpan.MaxValue;

        Stopwatch stopwatch = new Stopwatch();

        // do the whole copy TimeCount times, find fastest time
        for (int retry = 0; retry < TimeCount; retry++)
        {
            stopwatch.Start();
            for (int iteration = 0; iteration < Iterations; iteration++)
            {
                result = cloneFunction(sourceArray);
            }
            stopwatch.Stop();
            if (stopwatch.Elapsed < minimumElapsedTime)
            {
                minimumElapsedTime = stopwatch.Elapsed;
            }
        }
        Console.WriteLine("{0}: {1} seconds", label, minimumElapsedTime);
    }

    public static void TimeDifferentCopyApproaches()
    {
        Console.WriteLine("Points, Iterations: {0} {1}", Points, Iterations);
        Point[] sourceArray = new Point[Points];
        for (int index = 0; index < Points; index++)
        {
            sourceArray[index] = new Point(3, 5);
        }

        TimeFunction(sourceArray, Point.ClonePointArrayCopyMemory, "Memcpy");
        TimeFunction(sourceArray, Point.ClonePointArrayUnsafe, "Unsafe");
        TimeFunction(sourceArray, Point.ClonePointArray, "Baseline");
    }
}

