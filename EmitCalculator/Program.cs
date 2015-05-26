using System;
using System.Reflection.Emit;

namespace FifthTask
{
    class EmitCalculator
    {
        private string expr;
        private DynamicMethod dyn;
        private ILGenerator gen;
        private Type[] argvTypes = {typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)};
        public Func<int, int, int, int, int> Calculation;
        // public delegate int Calculation(int a, int b, int c, int d);

        public EmitCalculator(string expr)
        {
            this.dyn = new DynamicMethod("Calculate",  // name of dinamic method
                                         typeof(int),  // return type
                                         argvTypes,    // parameter types
                                         typeof(int).Module); // the type with which the dynamic method is logically associated
            this.gen = dyn.GetILGenerator();
            gen.Emit(OpCodes.Ldloc, this.expr);
            // this.GenerateCode();
            Calculation calculation = (Calculation) dyn.CreateDelegate(typeof(Calculation));
        }

        // private void GenerateCode()
        // {
        //     Label beginLoop = gen.DefineLabel();
        //     Label endLoop = gen.DefineLabel();
        //     gen.Emit(OpCodes.Ldc_I4, 13);
        //     // gen.MarkLabel(beginLoop);

        //     // gen.MarkLabel(endLoop);
        //     gen.Emit(OpCodes.Ret);
        // }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EmitCalculator calculator = new EmitCalculator("a b * c + d +");
            calculator.calculation(2, 3, 4, 3);
        }
    }
}
