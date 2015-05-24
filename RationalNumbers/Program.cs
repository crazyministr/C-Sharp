using System;

namespace FirstTask
{
    public struct RationalNumber
    {
        private int numerator;
        private int denominator;
        private int integerPart;

        public RationalNumber(int integerPart, int numerator, int denominator = 1)
        {
            if (denominator == 0)
                throw new ArithmeticException("Denominator must not be 0");

        	this.integerPart = integerPart;
            this.numerator = numerator;
            this.denominator = denominator;
            this.signOptimize();
        }

        private void signOptimize()
        {
            int signNumerator = 0, signDenominator = 0, signIntegerPart = 0;
            if (this.numerator < 0) signNumerator = 1;
            if (this.denominator < 0) signDenominator = 1;
            if (this.integerPart < 0) signIntegerPart = 1;
            this.numerator = Math.Abs(this.numerator);
            this.denominator = Math.Abs(this.denominator);
            this.integerPart = Math.Abs(this.integerPart);
            int s = signNumerator + signDenominator + signIntegerPart;
            if (s == 1 || s == 3)
                this.numerator *= -1;
        }

        private static int Gcd(int a, int b)
        {
            bool signA = a < 0;
            bool signB = b < 0;

            if (signA) a *= -1;
            if (signB) b *= -1;

            while (a != 0 && b != 0) if (a > b) a = a % b; else b = b % a;
            return signA == signB ? a + b : -(a + b);
        }

        public static RationalNumber operator +(RationalNumber a, RationalNumber b)
        {
            RationalNumber x = a.toImproperFraction();
            RationalNumber y = b.toImproperFraction();
            int numerator = x.numerator * y.denominator + x.denominator * y.numerator;
            int denominator = x.denominator * y.denominator;
            int gcd = Gcd(numerator, denominator);
            numerator /= gcd;
            denominator /= gcd;
            RationalNumber res = new RationalNumber(1, numerator, denominator);
            return res.toMixedFraction();
        }

        public static RationalNumber operator -(RationalNumber x, RationalNumber y)
        {
            y.numerator *= -1;
            return x + y;
        }

        public static RationalNumber operator *(RationalNumber a, RationalNumber b)
        {
            RationalNumber x = a.toImproperFraction();
            RationalNumber y = b.toImproperFraction();
            int numerator = x.numerator * y.numerator;
            int denominator = x.denominator * y.denominator;
            int gcd = Gcd(numerator, denominator);
            numerator /= gcd;
            denominator /= gcd;
            RationalNumber res = new RationalNumber(1, numerator, denominator);
            return res.toMixedFraction();
        }

        public static RationalNumber operator /(RationalNumber a, RationalNumber b)
        {
            RationalNumber y = b.toImproperFraction();
            if (y.numerator < 0)
            {
                y.numerator *= -1;
                y.denominator *= -1;
            }
            RationalNumber t = new RationalNumber(y.integerPart, y.denominator, y.numerator);
            return (a * t).toMixedFraction();
        }

        public override string ToString()
        {
            char ch = (this.numerator < 0) ? '-' : ' ';
            return String.Format("{0}{1} * {2} / {3}", ch, this.integerPart, Math.Abs(this.numerator), this.denominator);
        }

        public RationalNumber toImproperFraction()
        {
            if (this.integerPart < 2)
                return this;
            RationalNumber res = new RationalNumber();
            res.numerator = this.denominator * this.integerPart + Math.Abs(this.numerator);
            res.integerPart = 1;
            res.denominator = this.denominator;
            if (this.numerator < 0)
                res.numerator *= -1;
            return res;
        }

        public RationalNumber toMixedFraction()
        {
            RationalNumber res = this.toImproperFraction();
            if (res.numerator == res.denominator)
            {
                res.numerator = res.denominator = 1;
            } else if (res.numerator == 0)
            {
                res.denominator = 1;
            } else {
                int gcd = Math.Abs(Gcd(res.numerator, res.denominator));
                res.numerator /= gcd;
                res.denominator /= gcd;
            }
            return res;
            // if (Math.Abs(res.numerator) < res.denominator)
            //     return res;

            // res.integerPart = res.numerator / res.denominator;
            // if (res.integerPart == 0)
            //     res.integerPart = 1;
            // res.numerator %= res.denominator;
            // return res;
        }

        public double toDouble()
        {
            return (double) this.integerPart * this.numerator / this.denominator;
        }

        public decimal toDecimal()
        {
            return (decimal) this.integerPart * this.numerator / this.denominator;
        }
    }

    class Program
    {
        public static void TestAdd()
        {
            Console.WriteLine("=== TestAdd ===\n");
            RationalNumber result = new RationalNumber(-1, -1, -2) + new RationalNumber(1, 1, 4);
            RationalNumber answer = new RationalNumber(-1, 1, 4);
            Console.WriteLine(Math.Abs(result.toDouble() - answer.toDouble()) < 1e-5);

            result = new RationalNumber(1, 1, 2) + new RationalNumber(1, 1, 4);
            answer = new RationalNumber(1, 3, 4);
            Console.WriteLine(Math.Abs(result.toDouble() - answer.toDouble()) < 1e-5);
        }

        public static void TestSubtract()
        {
            Console.WriteLine("=== TestSubtract ===\n");
            RationalNumber result = new RationalNumber(1, 1, 2) - new RationalNumber(1, 1, 4);
            RationalNumber answer = new RationalNumber(1, 1, 4);
            Console.WriteLine(Math.Abs(result.toDouble() - answer.toDouble()) < 1e-5);
        }

        public static void TestMultiply()
        {
            Console.WriteLine("=== TestMultiply ===\n");
            RationalNumber result = new RationalNumber(1, 1, 2) * new RationalNumber(1, 1, 4);
            RationalNumber answer = new RationalNumber(1, 1, 8);
            Console.WriteLine(Math.Abs(result.toDouble() - answer.toDouble()) < 1e-5);
        }

        public static void TestDivide()
        {
            Console.WriteLine("=== TestDivide ===\n");
            RationalNumber result = new RationalNumber(1, 1, 2) / new RationalNumber(1, 1, 4);
            RationalNumber answer = new RationalNumber(2, 1, 1);
            Console.WriteLine(Math.Abs(result.toDouble() - answer.toDouble()) < 1e-5);
        }

        static public void Main()
        {
            TestAdd();
            TestSubtract();
            TestMultiply();
            TestDivide();
        }
    }
}
