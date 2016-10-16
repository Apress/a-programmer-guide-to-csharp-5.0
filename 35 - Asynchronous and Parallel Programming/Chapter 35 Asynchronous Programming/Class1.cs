using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_35_Asynchronous_Programming
{
    public class Class1
    {
        public static void Main()
        {
            SmugMugFeed smugMugFeed = new SmugMugFeed("popular");
            smugMugFeed.ProcessFeed();


        }


    }


}
