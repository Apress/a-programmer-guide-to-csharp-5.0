// 04 - Exception Handling\Passing Exceptions on to the Caller\Caller Inform
// copyright 2000 Eric Gunnerson
using System;
public class Summer
{
    int    sum = 0;
    int    count = 0;
    float    average;
    public void DoAverage()
    {
        try
        {
            average = sum / count;
        }
        catch (DivideByZeroException e)
        {
            // wrap exception in another one,
            // adding additional context.
            throw (new DivideByZeroException(
            "Count is zero in DoAverage()", e));
        }
    }
}
public class Test
{
    public static void Main()
    {
        Summer summer = new Summer();
        try
        {
            summer.DoAverage();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e);
        }
    }
}