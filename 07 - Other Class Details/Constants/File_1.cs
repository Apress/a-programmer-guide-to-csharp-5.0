// 08 - Other Class Details\Constants
// copyright 2000 Eric Gunnerson
using System;
enum MyEnum
{
    Jet
}
class LotsOLiterals
{
    // const items can't be changed.
    // const implies static. 
    public const int value1 = 33;
    public const string value2 = "Hello";
    public const MyEnum value3 = MyEnum.Jet;
}
class Test
{
    public static void Main()
    {
        Console.WriteLine("{0} {1} {2}", 
        LotsOLiterals.value1, 
        LotsOLiterals.value2, 
        LotsOLiterals.value3);
    }
}