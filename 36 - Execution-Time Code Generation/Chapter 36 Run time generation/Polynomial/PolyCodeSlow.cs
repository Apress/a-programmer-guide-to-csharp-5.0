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
    class PolyCodeSlow : Polynomial, IPolynomial
    {
        public PolyCodeSlow(params double[] coefficients)
            : base(coefficients)
        {
        }

        void WriteCode()
        {
            string timeString = m_polyNumber.ToString();
            m_polyNumber++;

            string filename = "PS_" + timeString;
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
            t.WriteLine("class {0}", className);
            t.WriteLine("{");
            t.WriteLine("public double Eval(double value)");
            t.WriteLine("{");
            t.WriteLine("    return(");
            t.WriteLine("        {0}", m_coefficients[0]);

            string closing = "";
            for (int i = 1; i < m_coefficients.Length; i++)
            {
                t.WriteLine("        + value * ({0} ", m_coefficients[i]);
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
            compParams.ReferencedAssemblies.AddRange(
                new string[]
                {
                    Path.GetFileName(Assembly.GetExecutingAssembly().CodeBase), 
                        "mscorlib.dll",
                        "System.dll"
                });
            compParams.IncludeDebugInformation = false;
            compParams.GenerateInMemory = false;
            compParams.OutputAssembly = (filename + ".dll");
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerResults res = provider.CompileAssemblyFromFile(compParams,
                filename + ".cs");

            // Open the file, and get a pointer to the method info
            Assembly a = Assembly.LoadFrom(filename + ".dll");

            m_func = a.CreateInstance(className);

            m_invokeType = a.GetType(className);

            File.Delete(filename + ".cs");
        }
        public override IPolynomial GetEvaluate()
        {
            WriteCode();
            return (IPolynomial)this;
        }

        public override double Evaluate(double value)
        {
            object[] args = new Object[] { value };
            object retValue =
                m_invokeType.InvokeMember("Eval",
                        BindingFlags.Default | BindingFlags.InvokeMethod,
                        null,
                        m_func,
                        args);
            return (double)retValue;
        }

        object m_func = null;
        Type m_invokeType = null;

        static int m_polyNumber = 0;    // which number we're using...
    }
}

