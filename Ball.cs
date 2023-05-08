using System;
using System.Drawing;

namespace Balls
{
    public class Ball
    {
        public enum Types {
            Regular,
            Monster,
            Repellent
        }

        private static Random rnd = new Random();

        public int x, y, dx, dy;
        public Types type;
        public Color color;
        private int minX, minY, maxX, maxY;
        public bool radiusChanged;


        private int radius;
        public int Radius
        {
            get { return radius; }
            set
            { 
                radius = Math.Max(2, Math.Min(Math.Min(maxX/2, maxY/2), value));
                radiusChanged = true;
            }
        }

        // specific constructor for manual adding
        public Ball(Types type, int x, int y, int minX, int minY, int maxX, int maxY)
        {
            this.type = type;

            this.radius = rnd.Next(5, 10);
            this.x = x;
            this.y = y;
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;

            if (type == Types.Monster)
            {
                this.dx = 0;
                this.dy = 0;
                this.color = Color.Black;
            }
            else
            {
                this.dx = rnd.Next(-3, 3);
                this.dy = rnd.Next(-3, 3);

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

        //constructor for random generating
        public Ball(Types type, int minX, int minY, int maxX, int maxY)
        {
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
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
                this.dx = rnd.Next(-3, 3);
                this.dy = rnd.Next(-3, 3);

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

        public void Draw(Graphics handler)
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

        public void Tick()
        {
            this.x += this.dx;
            this.y += this.dy;
        }
    }
}