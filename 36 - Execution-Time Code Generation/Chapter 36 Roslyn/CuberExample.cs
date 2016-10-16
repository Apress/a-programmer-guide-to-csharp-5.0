using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.CSharp;
using System.Reflection;
using System.IO;

class CuberGenerator
{
    static string CubedExpression(double x)
    {
        return x.ToString() + " * " + x.ToString() + " * " + x.ToString();
    }

    public static Func<double> GetCuber(double x)
    {
        string program =
            @"class Cuber {public static double Cubed() { return " +
            CubedExpression(x) +
            "; } }";

        Console.WriteLine(program);

        SyntaxTree tree = SyntaxTree.ParseCompilationUnit(program);

        Compilation compilation = Compilation.Create(
                            "CuberGenerator.dll",
                            new CompilationOptions(OutputKind.DynamicallyLinkedLibrary),
                            new[] { tree },
                            new[] { new AssemblyFileReference(typeof(object).Assembly.Location) });

        Assembly compiledAssembly;
        using (var stream = new MemoryStream())
        {
            EmitResult compileResult = compilation.Emit(stream);
            compiledAssembly = Assembly.Load(stream.GetBuffer());
        }

        Type cuber = compiledAssembly.GetType("Cuber");
        MethodInfo getValue = cuber.GetMethod("Cubed");

        Func<double> cubedValue = (Func<double>)Delegate.CreateDelegate(typeof(Func<double>), getValue);
        return cubedValue;
    }
}
