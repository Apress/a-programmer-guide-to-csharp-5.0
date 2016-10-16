using System;
using System.Reflection;
using System.Reflection.Emit;


namespace Polynomial
{
 class LightWeightPoly
 {
  public void Eval()
  {
   // Evaluate the first polynomial, with 7 elements
   double[] coeff = new double[] { 5.5, 7.0, 15, 30, 500, 100, 1 };
   DynamicMethod dm = GetEvaluator(coeff);

   object[] parameter = new object[] { 2.0 };
   double result = (double)dm.Invoke(null, parameter);
  }

  DynamicMethod GetEvaluator(params double[] coefficients)
  {
   //define dynamic method contruction data
   Type[] paramTypes = new Type[] { typeof(double) };
   Type returnType = typeof(double);
   Type methodOwner = this.GetType();
   //
   //create dynamic method
   DynamicMethod dm = new DynamicMethod("Evaluate", returnType, paramTypes, methodOwner, false);
   ILGenerator il = dm.GetILGenerator();
   //
   // Emit the IL. This is a hand-coded version of what
   // you'd get if you compiled the code example and then ran
   // ILDASM on the output.
   //
   //
   // This first section repeated loads the coefficients
   // x value on the stack for evaluation.
   //
   for (int index = 0; index < coefficients.Length - 1; index++)
   {
    il.Emit(OpCodes.Ldc_R8, coefficients[index]);
    il.Emit(OpCodes.Ldarg_1);
   }
   // load the last coefficient
   il.Emit(OpCodes.Ldc_R8, coefficients[coefficients.Length - 1]);
   // Emit the remainder of the code. This is a repeated
   // section of multiplying the terms together and
   // accumulating them.
   for (int loop = 0; loop < coefficients.Length - 1; loop++)
   {
    il.Emit(OpCodes.Mul);
    il.Emit(OpCodes.Add);
   }
   // return the value
   il.Emit(OpCodes.Ret);

   //dynamic method now done - return it
   return dm;
  }
 }
}
