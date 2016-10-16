namespace Polynomial
{
 using System;
 using System.IO;
 using System.Diagnostics;
 using System.Reflection;
 using PolyInterface;
 using System.CodeDom;
 using System.CodeDom.Compiler;
 using Microsoft.CSharp;

 class PolyCodeDom : Polynomial
 {
  public PolyCodeDom(params double[] coefficients)
   : base(coefficients)
  {
  }
  void WriteCode()
  {
   string timeString = polyNumber.ToString();
   polyNumber++;
   string filename = "PSCD_" + timeString;
   Stream s = File.Open(filename + ".cs", FileMode.Create);
   StreamWriter t = new StreamWriter(s);
   // Generate code in C#
   CSharpCodeProvider provider = new CSharpCodeProvider();
   ICodeGenerator cg = provider.CreateGenerator(t);
   CodeGeneratorOptions op = new CodeGeneratorOptions();
   // Generate the comments at the beginning of the function
   CodeCommentStatement comment =
   new CodeCommentStatement("Polynomial evaluator");
   cg.GenerateCodeFromStatement(comment, t, op);
   string[] terms = new string[coefficients.Length];
   terms[0] = coefficients[0].ToString();
   for (int i = 1; i < coefficients.Length; i++)
    terms[i] = String.Format("{0} X^{1}", coefficients[i], i);
   comment = new CodeCommentStatement(
   "Evaluating Y = " + String.Join(" + ", terms));
   cg.GenerateCodeFromStatement(comment, t, op);
   // The class is named with a unique name
   string className = "Poly_" + timeString;
   CodeTypeDeclaration polyClass = new CodeTypeDeclaration(className);
   // The class implements IPolynomial
   polyClass.BaseTypes.Add("PolyInterface.IPolynomial");
   // Set up the Eval function
   CodeParameterDeclarationExpression param1 =
   new CodeParameterDeclarationExpression("System.Double", "x");
   CodeMemberMethod eval = new CodeMemberMethod();
   eval.Name = "Evaluate";
   eval.Parameters.Add(param1);
   // workaround for bug below...
   eval.ReturnType = new CodeTypeReference("public double");
   // BUG: This doesn't generate "public", it just leaves
   // the attribute off of the member...
   eval.Attributes |= MemberAttributes.Public;
   // Create the expression to do the evaluation of the
   // polynomail. To do this, we chain together binary
   // operators to get the desired expression
   // a0 + x * (a1 + x * (a2 + x * (a3)));
   //
   // This is very much like building a parse tree for
   // an expression.
   CodeBinaryOperatorExpression plus = new CodeBinaryOperatorExpression();
   plus.Left = new CodePrimitiveExpression(coefficients[0]);
   plus.Operator = CodeBinaryOperatorType.Add;
   CodeBinaryOperatorExpression current = plus;
   for (int i = 1; i < coefficients.Length; i++)
   {
    CodeBinaryOperatorExpression multiply =
    new CodeBinaryOperatorExpression();
    current.Right = multiply;
    multiply.Left = new CodeSnippetExpression("x");
    multiply.Operator = CodeBinaryOperatorType.Multiply;
    CodeBinaryOperatorExpression add = new CodeBinaryOperatorExpression();
    multiply.Right = add;
    add.Operator = CodeBinaryOperatorType.Add;
    add.Left = new CodePrimitiveExpression(coefficients[i]);
    current = add;
   }
   current.Right = new CodePrimitiveExpression(0.0);
   // return the expression...
   eval.Statements.Add(new CodeMethodReturnStatement(plus));
   polyClass.Members.Add(eval);
   cg.GenerateCodeFromType(polyClass, t, op);
   t.Close();
   s.Close();

   //compile the DLL
   CompilerParameters compParams = new CompilerParameters();
   compParams.CompilerOptions = "/target:library /o+";
   compParams.ReferencedAssemblies.AddRange(new string[] 
    {Path.GetFileName(Assembly.GetExecutingAssembly().CodeBase), 
     "mscorlib.dll",
     "System.dll"});
   compParams.IncludeDebugInformation = false;
   compParams.GenerateInMemory = false;
   compParams.OutputAssembly = (filename + ".dll");
   CompilerResults res = provider.CompileAssemblyFromFile(compParams, filename + ".cs");

    /*
   ProcessStartInfo psi = new ProcessStartInfo();
   psi.FileName = "cmd.exe";
   psi.Arguments = String.Format(
   "/c csc /o+ /r:polynomial.exe /target:library {0}.cs > compile.out",
   filename);
   psi.WindowStyle = ProcessWindowStyle.Minimized;
   Process proc = Process.Start(psi);
   proc.WaitForExit();
   // Open the file, create the instance, and cast it
   // to the assembly
     * */

   Assembly a = Assembly.LoadFrom(filename + ".dll");
   polynomial = (IPolynomial)a.CreateInstance(className);
   File.Delete(filename + ".cs");
  }
  public override IPolynomial GetEvaluate()
  {
   if (polynomial == null)
    WriteCode();
   return ((IPolynomial)polynomial);
  }
  public override double Evaluate(double value)
  {
   return (0.0); // not used
  }
  IPolynomial polynomial = null;
  static int polyNumber = 1000;
 }
}