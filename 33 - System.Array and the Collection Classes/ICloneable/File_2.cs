// 28 - System.Array and the Collection Classes\ICloneable
// copyright 2000 Eric Gunnerson
using System;
class ContainedValue
{
    public ContainedValue(int count)
    {
        this.count = count;
    }
    public int count;
}
class MyObject: ICloneable
{
    public MyObject(int count)
    {
        this.contained = new ContainedValue(count);
    }
    public object Clone()
    {
        Console.WriteLine("Clone");
        return(new MyObject(this.contained.count));
    }
    public ContainedValue contained;
}
class Test
{
    public static void Main()
    {
        MyObject    my = new MyObject(33);
        MyObject    myClone = (MyObject) my.Clone();
        Console.WriteLine(    "Values: {0} {1}",
        my.contained.count,
        myClone.contained.count);
        myClone.contained.count = 15;
        Console.WriteLine(    "Values: {0} {1}",
        my.contained.count,
        myClone.contained.count);
    }
}