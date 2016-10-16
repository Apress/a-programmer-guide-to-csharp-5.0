using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_38_Framework
{
class RoundTrip
{
    public static void Mainly()
    {
        Random random = new Random();
        while (true)
        {
            double value = random.NextDouble();

            if (value != ToStringAndBack(value))
            {
                Console.WriteLine("Different: {0}", value);
            }
        }
    }
    public static double ToStringAndBack(double value)
    {
        string valueAsString = value.ToString("R");

        return Double.Parse(valueAsString);
    }
}
}
