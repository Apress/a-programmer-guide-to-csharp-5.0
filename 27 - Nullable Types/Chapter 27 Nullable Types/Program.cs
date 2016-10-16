using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_27_Nullable_Types
{
    class Program
    {
        static void Main(string[] args)
        {
            IntegerExample();
            NullableExample();
            NullableExpressions();
            Comparisons();
        }

static void IntegerExample()
{
    Integer value = 15;
    Process(value);
    Integer nullInteger = Integer.Null;
    Process(nullInteger);
}
static void Process(Integer integer)
{
    Console.WriteLine(integer.HasValue ? integer.Value.ToString() : "null");
}

static void NullableExample()
{
    int? value = 15;
    Process(value);
    int? nullValue = null;
    Process(nullValue);
}

static void Process(int? value)
{
    Console.WriteLine(value.HasValue ? value.Value.ToString() : "null");
}

static int ValueOrFifteen(int? value)
{
    int final;
    final = value.HasValue ? value.Value : 15;
    final = value.GetValueOrDefault(15);
    final = value ?? 15;
    return final;
}


static void NullableExpressions()
{
    int? i = 10;
    int? j = 20;
    int? n = null;

    int? k = i * j;

    int? s = k + n;
    Console.WriteLine(k);
    Console.WriteLine(s);


}

static void Comparisons()
{
    int? i = null;
    int? j = null;
    int? k = 15;

    string s = null;
    string t = null;

    Console.WriteLine(i == j);

    Console.WriteLine(s == t);


    Console.WriteLine(j > k);
    Console.WriteLine(j < k);

}


    }
}
