// 04 - Exception Handling\Finally
// copyright 2000 Eric Gunnerson
using System;
using System.IO;
class Processor
{
    int    count;
    int    sum;
    public int average;
    void CalculateAverage(int countAdd, int sumAdd)
    {
        count += countAdd;
        sum += sumAdd;
        average = sum / count;
    }    
    public void ProcessFile()
    {
        FileStream f = new FileStream("data.txt", FileMode.Open);
        try
        {
            StreamReader t = new StreamReader(f);
            string    line;
            while ((line = t.ReadLine()) != null)
            {
                int count;
                int sum;
                count = Convert.ToInt32(line);
                line = t.ReadLine();
                sum = Convert.ToInt32(line);
                CalculateAverage(count, sum);
            }
        }
        // always executed before function exit, even if an
        // exception was thrown in the try.
        finally
        {
            f.Close();
        }
    }
}
class Test
{
    public static void Main()
    {
        Processor processor = new Processor();
        try
        {
            processor.ProcessFile();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e);
        }
    }
}