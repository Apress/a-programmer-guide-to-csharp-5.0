using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_16___Generic_Types
{
    public class ReferenceArrayCovariance
    {
        public static void ReferenceCovariance()
        {
            Sedan dodgeDart = new Sedan();

            Auto currentCar = dodgeDart;

        }

public static void ArrayCovariance()
{
    Sedan[] sedans = new Sedan[1];
    sedans[0] = new Sedan();

    Auto[] autos = sedans;

    autos[0] = new Roadster();
}
    }
}
