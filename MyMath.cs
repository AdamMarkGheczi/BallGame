using System;
using System.Drawing;

namespace Balls
{
    public static class MyMath
    {
        public static double SolveQuadratic(int a, int b, int c)
        {
            int commonFactor = gcd(a, gcd(b, c));

            a /= commonFactor;
            b /= commonFactor;
            c /= commonFactor;

            int delta = b * b - 4 * a * c;

            if (delta >= 0) return (-b - Math.Sqrt(delta)) / (2 * a);

            return double.MinValue;
        }

        public static int gcd(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            if (a < b) (a, b) = (b, a);

            if (a == b && a == 0) return 1;
            if (b == 0) return a;
            if (a == b) return a;

            int x;

            while (true)
            {
                x = a % b;
                if (x == 0) return b;
                a = b;
                b = x;
            }
        }

        public static Color CombineColorByWeights(Color a, int weightA, Color b, int weightB)
        {
            int red = (a.R * weightA + b.R * weightB) / (weightA + weightB);
            int grn = (a.G * weightA + b.G * weightB) / (weightA + weightB);
            int blu = (a.B * weightA + b.B * weightB) / (weightA + weightB);

            return Color.FromArgb(red, grn, blu);
        }
    }
}
