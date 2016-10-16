using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolynomialBase;

namespace PolynomialImplementation
{
    class PolyBrokenFastest : Polynomial, IPolynomial
    {
        public PolyBrokenFastest(params double[] coefficients)
            : base(coefficients)
        {
        }

        public override IPolynomial GetEvaluate()
        {
            return (IPolynomial) this;
        }

        public override double Evaluate(double value)
        {
            return 3D;
        }
    }
}
