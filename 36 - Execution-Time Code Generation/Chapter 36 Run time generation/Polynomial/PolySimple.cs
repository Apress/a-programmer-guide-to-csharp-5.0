using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolynomialBase;

namespace PolynomialImplementation
{
    /// <summary>
    /// The simplest polynomial implementation
    /// </summary>
    /// <description>
    /// This implementation loops through the coefficients and evaluates each
    /// term of the polynomial.
    /// </description>
    class PolySimple : Polynomial, IPolynomial
    {
        public PolySimple(params double[] coefficients)
            : base(coefficients)
        {
        }

        public override IPolynomial GetEvaluate()
        {
            return (IPolynomial) this;
        }

        public override double Evaluate(double value)
        {
            double retval = m_coefficients[0];

            double f = value;

            for (int i = 1; i < m_coefficients.Length; i++)
            {
                retval += m_coefficients[i] * f;
                f *= value;
            }
            return retval;
        }
    }
}

