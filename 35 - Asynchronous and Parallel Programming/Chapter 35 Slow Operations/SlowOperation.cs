using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_35_Slow_Operations
{
class SlowOperation
{
    public static int Process()
    {
        int sum = 0;

        for (int i = 0; i < 1000000000; i++)
        {
            sum += i;
        }

        return sum;
    }
}
}
