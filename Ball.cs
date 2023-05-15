using System;
using System.Collections.Generic;
using System.Drawing;

namespace Balls
{
    public class Ball
    {
        public enum Types {
            Regular,
            Repellent,
            Monster
        }
        
        private static Random rnd = new Random();
        private static int minX, minY, maxX, maxY;

        public int x, y, dx, dy;
        public Types type;
        public Color color;

        public bool radiusChanged;

        private int radius;
        public int Radius
        {
            get { return radius; }
            set
            {
                radius = Math.Min(Math.Min(maxX / 2, maxY / 2), value);
                radiusChanged = true;
            }
        }

        /// <summary>
        /// Sets the bounds in which the balls can be generated and added
        /// </summary>
        public static void SetBounds(int minX, int minY, int maxX, int maxY)
        {
            Ball.minX = minX;
            Ball.minY = minY;
            Ball.maxX = maxX;
            Ball.maxY = maxY;
        }

        /// <summary>
        /// Specific constructor for manual adding
        /// </summary>
        public Ball(Types type, int x, int y)
        {
            if (x < minX || x > maxX || y < minY || y > maxY) return;

            this.type = type;

            this.radius = rnd.Next(5, 10);
            this.x = x;
            this.y = y;

            if (type == Types.Monster)
            {
                this.dx = 0;
                this.dy = 0;
                this.color = Color.Black;
            }
            else
            {
                int[] velocities = MyMath.MaxOneZeroRnd(-3, 3);
                this.dx = velocities[0];
                this.dy = velocities[1];

                int r, g, b;
                do
                {
                    r = rnd.Next(255);
                    g = rnd.Next(255);
                    b = rnd.Next(255);
                } while ((r + g + b) / 3 < 50);

                this.color = Color.FromArgb(r, g, b);
            }
        }

        /// <summary>
        /// Constructor for randomly generating
        /// </summary>
        public Ball(Types type)
        {
            this.type = type;

            this.radius = rnd.Next(5, 10);
            this.x = rnd.Next(minX + radius / 2, maxX - radius / 2);
            this.y = rnd.Next(minY + radius / 2, maxY - radius / 2);

            if(type == Types.Monster)
            {
                this.dx = 0;
                this.dy = 0;
                this.color = Color.Black;
            }
            else
            {
                int[] velocities = MyMath.MaxOneZeroRnd(-3, 3);
                this.dx = velocities[0];
                this.dy = velocities[1];

                int r, g, b;
                do
                {
                    r = rnd.Next(255);
                    g = rnd.Next(255);
                    b = rnd.Next(255);
                } while ((r + g + b) / 3 < 50);

                this.color = Color.FromArgb(r, g, b);
            }
        }

        public void Draw(Graphics handler, List<Ball> balls)
        {
            handler.FillEllipse(new SolidBrush(color), x - radius, y - radius, 2*radius, 2*radius);

            if(type==Types.Monster)
            {
                handler.DrawEllipse(new Pen(Color.Purple, 3), x - radius, y - radius, 2 * radius, 2 * radius);
            }

            if (type == Types.Repellent)
            {
                handler.DrawEllipse(new Pen(Color.Black, 2), x - radius, y - radius, 2 * radius, 2 * radius);
            }
        }

        /// <summary>
        /// Advances the ball
        /// </summary>
        public void Tick()
        {
            this.x += this.dx;
            this.y += this.dy;
        }

        public override string ToString()
        {
            return $"{(int)type} {x} {y} {dx} {dy} {radius}";
        }
    }
}