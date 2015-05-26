using System;
using System.Reflection.Emit;

namespace FifthTask
{
    class EmitCalculator
    {
        private string[] tokens;
        private DynamicMethod dyn;
        private ILGenerator gen;
        private Type[] argvTypes = {typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)};
        // private Func<int, int, int, int, int> Calculation;
        private delegate int Calculation(int a, int b, int c, int d, int ret);

        public EmitCalculator(string expr, int a, int b, int c, int d)
        {
            this.dyn = new DynamicMethod("Calculate",  // name of dinamic method
                                         typeof(int),  // return type
                                         argvTypes,    // parameter types
                                         typeof(int)); // the type with which the dynamic method is logically associated

            this.tokens = expr.Split(new Char[] {' '});
            this.gen = dyn.GetILGenerator();
            this.GenerateCode();
            Calculation calculation = (Calculation) dyn.CreateDelegate(typeof(Calculation));
            Console.WriteLine(calculation(a, b, c, d, 0));
        }

        private void GenerateCode()
        {
            foreach (string token in this.tokens)
            {
                if (token.Equals("+"))
                    gen.Emit(OpCodes.Add);
                else if (token.Equals("*"))
                    gen.Emit(OpCodes.Mul);
                else
                {
                    if (token.Equals("a"))
                        gen.Emit(OpCodes.Ldarg_0);
                    else if (token.Equals("b"))
                        gen.Emit(OpCodes.Ldarg_1);
                    else if (token.Equals("c"))
                        gen.Emit(OpCodes.Ldarg_2);
                    else if (token.Equals("d"))
                        gen.Emit(OpCodes.Ldarg_3);
                }
            }
            gen.Emit(OpCodes.Ret);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EmitCalculator calculator = new EmitCalculator("a b * c + d +", 2, 3, 4, 3);
        }
    }
}
