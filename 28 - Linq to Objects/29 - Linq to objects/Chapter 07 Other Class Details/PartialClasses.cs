using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_07_Other_Class_Details
{
partial class Saluter
{
    int m_saluteCount;

    public Saluter(int saluteCount)
    {
        m_saluteCount = saluteCount;
    }

    public void Ready()
    {
        Console.WriteLine("Ready");
    }
}
partial class Saluter
{
    public void Aim()
    {
        Console.WriteLine("Aim");
    }
}
partial class Saluter
{
    public void Fire()
    {
        for (int i = 0; i < m_saluteCount; i++)
        {
            Console.WriteLine("Fire");
        }
    }
}
}
