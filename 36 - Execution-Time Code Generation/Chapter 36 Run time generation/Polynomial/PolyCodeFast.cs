using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.CodeDom.Compiler;
using PolynomialBase;

namespace PolynomialImplementation
{
    class PolyCodeFast : Polynomial, IPolynomial
    {
        public PolyCodeFast(params double[] coefficients)
            : base(coefficients)
        {
        }

        void WriteCode()
        {
            string timeString = m_polyNumber.ToString();
            m_polyNumber++;

            string filename = "PSF_" + timeString;
            Stream s = File.Open(filename + ".cs", FileMode.Create);
            StreamWriter t = new StreamWriter(s);

            t.WriteLine("// polynomial evaluator");
            t.Write("// Evaluating y = ");

            string[] terms = new string[m_coefficients.Length];
            terms[0] = m_coefficients[0].ToString();

            for (int i = 1; i < m_coefficients.Length; i++)
            {
                terms[i] = String.Format("{0} X^{1}", m_coefficients[1], i);
            }

            t.Write("{0}", String.Join(" + ", terms));
            t.WriteLine();

            t.WriteLine("");

            string className = "Poly_" + timeString;
            t.WriteLine("class {0}: PolynomialBase.IPolynomial", className);
            t.WriteLine("{");
            t.WriteLine("public double Evaluate(double value)");
            t.WriteLine("{");
            t.WriteLine("    return(");
            t.WriteLine("        {0}D", m_coefficients[0]);

            string closing = "";
            for (int i = 1; i < m_coefficients.Length; i++)
            {
                t.WriteLine("        + value * ({0}D ", m_coefficients[i]);
                closing += ")";
            }
            t.Write("\t{0}", closing);
            t.WriteLine(");");

            t.WriteLine("}");
            t.WriteLine("}");
            t.Close();
            s.Close();

            //compile the DLL
            CompilerParameters compParams = new CompilerParameters();

            compParams.CompilerOptions = "/target:library /o+";
            compParams.ReferencedAssemblies.Add("PolynomialBase.dll");
            compParams.IncludeDebugInformation = false;
            compParams.GenerateInMemory = false;
            compParams.GenerateExecutable = false;
            compParams.OutputAssembly = (filename + ".dll");
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerResults res = provider.CompileAssemblyFromFile(compParams,
                filename + ".cs");

            foreach (var error in res.Errors)
            {
                Console.WriteLine(error);
            }

            // Open the file, and get a pointer to the method info
            Assembly a = Assembly.LoadFrom(filename + ".dll");
            //Assembly a = res.CompiledAssembly;
            m_interface = (IPolynomial) a.CreateInstance(className);

            //File.Delete(filename + ".cs");
        }
        public override IPolynomial GetEvaluate()
        {
            WriteCode();
            return m_interface;

            //return (IPolynomial)this;
        }

        public override double Evaluate(double value)
        {
            return 0;
        }

        IPolynomial m_interface;
        static int m_polyNumber = 0;    // which number we're using...
    }
}

