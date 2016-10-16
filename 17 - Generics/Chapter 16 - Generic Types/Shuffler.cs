using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_16___Generic_Types
{
    public class Shuffler
    {
#if nongeneric
public static List<string> Shuffle(List<string> list1, List<string> list2)
{
    List<string> shuffled = new List<string>();

    for (int i = 0; i < list1.Count; i++)
    {
        shuffled.Add(list1[i]);
        shuffled.Add(list2[i]);
    }

    return shuffled;
}
#else
public static List<T> Shuffle<T>(List<T> list1, List<T> list2)
{
    List<T> shuffled = new List<T>();

    for (int i = 0; i < list1.Count; i++)
    {
        shuffled.Add(list1[i]);
        shuffled.Add(list2[i]);
    }

    return shuffled;
}    
#endif
    }
}
