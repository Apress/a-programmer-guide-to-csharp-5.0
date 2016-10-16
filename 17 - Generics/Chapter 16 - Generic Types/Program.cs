using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_16___Generic_Types
{
    class Program
    {
        static void Main(string[] args)
        {
            IntList intList = new IntList(5);
            intList.Add(15);
            intList.Add(22);

            Console.WriteLine(intList.Count);
            for (int i = 0; i < intList.Count; i++)
            {
                Console.WriteLine(intList[i]);
            }

            MyList<int> l = new MyList<int>(33);
            MyConstructedList<int> m;


            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();


            list1.Add("All");
            list1.Add("base");
            list1.Add("belong");
            list1.Add("us");

            list2.Add("your");
            list2.Add("are");
            list2.Add("to");
            list2.Add("!");

            foreach (string word in Shuffler.Shuffle<string>(list1, list2))
            {
                Console.WriteLine(word);
            }

            ReferenceArrayCovariance.ReferenceCovariance();
            ReferenceArrayCovariance.ArrayCovariance();
        }
    }

}
