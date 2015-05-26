using System;
using System.Reflection.Emit;

namespace FifthTask
{
    class EmitCalculator
    {
        private string expr;
        private DynamicMethod dyn;
        private ILGenerator gen;
        private Type[] argvTypes = {typeof(int), typeof(int), typeof(int), typeof(int)};
        private Func<int, int, int, int, int> Calculation;

        public EmitCalculator(string expr)
        {
            this.expr = expr;
            this.dyn = new DynamicMethod("Calculate",  // name of dinamic method
                                         typeof(int),  // return type
                                         argvTypes,    // parameter types
                                         typeof(int)); // the type with which the dynamic method is logically associated
            this.gen = dyn.GetILGenerator();
            this.GenerateCode();
            this.Calculation = dyn.CreateDelegate(typeof(Calculation));
        }

        public GenerateCode()
        {
            Label beginLoop = gen.DefineLabel();
            Label endLoop = gen.DefineLabel();
            
            gen.Emit(OpCodes.Ldstr, this.expr);
            gen.MarkLabel(beginLoop);

            gen.MarkLabel(endLoop);
            gen.Emit(OpCodes.Ret);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var calculator = EmitCalculator("a b * c + d +");
            calculator.Calculation(2, 3, 4, 3);
        }
    }
}
