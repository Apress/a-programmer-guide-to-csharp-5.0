using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialBase
{
        /// <summary>
        /// The abstract class all implementations inherit from
        /// </summary>
    public abstract class Polynomial
    {
        public Polynomial(params double[] coefficients)
        {
            m_coefficients = new double[coefficients.Length];
            coefficients.CopyTo(m_coefficients, 0);
        }

        public abstract double Evaluate(double value);
        public abstract IPolynomial GetEvaluate();

        protected double[] m_coefficients = null;
    }
}

