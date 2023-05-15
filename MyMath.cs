using System;
using System.Drawing;

namespace Balls
{
    public static class MyMath
    {
        private static Random rnd = new Random();
        /// <returns>
        /// The first root of the quadratic described by a, b and c.
        /// If there are no real roots the returned value is double.PositiveInfinity
        /// </returns>
        public static double FirstRootOfQuadratic(int a, int b, int c)
        {
            int commonFactor = Gcd(a, Gcd(b, c));

            a /= commonFactor;
            b /= commonFactor;
            c /= commonFactor;

            int delta = b * b - 4 * a * c;

            if (delta >= 0)
            {
                return (-b - Math.Sqrt(delta)) / (2 * a);
            }

            return double.PositiveInfinity;
        }

        /// <returns>The greatest common divisor of a and b. If both a and b are equal to 0, the returned value is 1</returns>
        public static int Gcd(int a, int b)
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

        /// <summary>
        /// Creates a new color by taking the weighted average of a and b.
        /// </summary>
        /// <returns>The resulting color from the weighted average of a and b</returns>
        public static Color CombineColorsByWeights(Color a, int weightA, Color b, int weightB)
        {
            int red = (a.R * weightA + b.R * weightB) / (weightA + weightB);
            int grn = (a.G * weightA + b.G * weightB) / (weightA + weightB);
            int blu = (a.B * weightA + b.B * weightB) / (weightA + weightB);

            return Color.FromArgb(red, grn, blu);
        }

        /// <summary>
        /// Generates a nonzero random integer in the specified interval
        /// </summary>
        /// <param name="lower">Inclusive lower bound</param>
        /// <param name="upper">Inclusive uppder bound</param>
        public static int NonZeroRnd(int lower, int upper)
        {
            int x = rnd.Next(lower, upper);

            if (x < 0) return x;
            else return x + 1;
        }

        /// <summary>
        /// Generates two numbers in the specified interval such that they can't be both equal to zero
        /// </summary>
        /// <param name="lower">Inclusive lower bound</param>
        /// <param name="upper">Inclusive uppder bound</param>
        public static int[] MaxOneZeroRnd(int lower, int upper)
        {
            int x = rnd.Next(lower, upper + 1);
            int y = rnd.Next(lower, upper + 1);

            if(x == 0 && y == 0)
            {
                int t = rnd.Next(0, 2);
                if(t == 0) x = NonZeroRnd(lower, upper);
                else y = NonZeroRnd(lower, upper);
            }
            
            return new int[] { x, y };

        }
    }
}
