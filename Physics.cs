using System;

namespace Balls
{
    public static class Physics
    {
        public static bool DiscreteCollision(Ball A, Ball B)
        {
            return Math.Sqrt((A.x - B.x) * (A.x - B.x) + (A.y - B.y) * (A.y - B.y)) <= A.Radius + B.Radius;
        }

        public static double ContinuousCollision(Ball A, Ball B)
        {
            int a = (A.x + A.dx) - (B.x + B.dx);
            int b = A.x - B.x;
            int c = (A.y + A.dy) - (B.y + B.dy);
            int d = A.y - B.y;
            int R = (A.Radius + B.Radius) * (A.Radius + B.Radius);

            int exp1 = a*a + b*b + c*c + d*d -2*a*b - 2*c*d;
            int exp2 = 2*a*b + 2*c*d - 2*b*b - 2*d*d;
            int exp3 = b*b + d*d - R;

            return MyMath.SolveQuadratic(exp1, exp2, exp3);
        }

        /// <summary>
        /// 1-Top 2-Right 3-Bottom 4-Left
        /// </summary>
        public static bool DoesBallCollideWithWall(Ball A, int wallPosition, int wallType)
        {
            switch (wallType)
            {
                case 1:
                    return A.y - A.Radius <= wallPosition;
                case 2:
                    return A.x + A.Radius >= wallPosition;
                case 3:
                    return A.y + A.Radius >= wallPosition;
                case 4:
                    return A.x - A.Radius <= wallPosition;
                default:
                    // so that all code path return a value and the compiler is happy
                    return false;
            }
            
        }

    }
}
