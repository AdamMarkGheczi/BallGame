using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Balls
{
    public class Engine
    {
        public Graphics graphicsHandler;
        public PictureBox screen;
        public Bitmap frame;
        public List<Ball> objects;
        public Engine(PictureBox screen)
        {
            objects = new List<Ball>();
            this.screen = screen;
            this.frame = new Bitmap(screen.Width, screen.Height);
            this.graphicsHandler = Graphics.FromImage(frame);
        }

        public void AddBall(Ball.Types type)
        {
            Ball currentBall;
            bool ok;

            do
            {
                currentBall = new Ball(type, 0, 0, screen.Width, screen.Height);
                ok = true;
                foreach (Ball otherBall in objects)
                {
                    if(Physics.DiscreteCollision(currentBall, otherBall))
                    {
                        ok = false;
                        break;
                    }
                } 
            } while (!ok);
            
            objects.Add(currentBall);
        }

        public void AddBall(Ball b)
        {
            objects.Add(b);
        }

        public void DeleteBalls()
        {
            objects.Clear();
        }

        struct collisionPair
        {
            public Ball A, B;
            public collisionPair(Ball A, Ball B)
            {
                this.A = A;
                this.B = B;
            }

            public static bool operator ==(collisionPair a, collisionPair b)
            {
                if(a.A == b.A && a.B == b.B) return true;
                if(a.A == b.B && a.B == b.A) return true;
                return false;
            }

            public static bool operator !=(collisionPair a, collisionPair b)
            {
                return !(a == b);
            }
        }

        public void TickBalls()
        {
            List<collisionPair> collisionPairs = new List<collisionPair>();
            List<collisionPair> nonCollisionPairs = new List<collisionPair>();
            // determining collisions
            foreach(Ball currentBall in objects)
            {
                foreach(Ball otherBall in objects)
                {
                    if (currentBall == otherBall) continue;

                    bool alreadyAccountedFor = false;
                    collisionPair cp = new collisionPair(currentBall, otherBall);
                    foreach(collisionPair pair in collisionPairs)
                    {
                        if(pair == cp)
                        {
                            alreadyAccountedFor = true;
                            break;
                        }
                    }

                    foreach (collisionPair pair in nonCollisionPairs)
                    {
                        if (pair == cp)
                        {
                            alreadyAccountedFor = true;
                            break;
                        }
                    }

                    if (alreadyAccountedFor) continue;

                    if (currentBall.radiusChanged || otherBall.radiusChanged)
                    {
                        if (Physics.DiscreteCollision(currentBall, otherBall))
                            collisionPairs.Add(cp);
                    }
                    else
                    {
                        double x = Physics.ContinuousCollision(currentBall, otherBall);

                        if (0 < x && x <= 1)
                            collisionPairs.Add(cp);
                        else
                            nonCollisionPairs.Add(cp);
                    }
                }

                currentBall.radiusChanged = false;

                #region Wall Collisions

                if (Physics.DoesBallCollideWithWall(currentBall, 0, 1))
                {
                    currentBall.dy = -currentBall.dy;
                    currentBall.y = currentBall.Radius;
                }

                if (Physics.DoesBallCollideWithWall(currentBall, screen.Width, 2))
                {
                    currentBall.dx = -currentBall.dx;
                    currentBall.x = screen.Width - currentBall.Radius;
                }
                if (Physics.DoesBallCollideWithWall(currentBall, screen.Height, 3))
                {
                    currentBall.dy = -currentBall.dy;
                    currentBall.y = screen.Height - currentBall.Radius;
                }
                if (Physics.DoesBallCollideWithWall(currentBall, 0, 4))
                {
                    currentBall.dx = -currentBall.dx;
                    currentBall.x = currentBall.Radius;
                }

                #endregion
            }

            foreach(collisionPair cp in collisionPairs)
            {
                Ball A = cp.A;
                Ball B = cp.B;

                int collisionType = 0;

                #region Collision Types
                if (A.type == Ball.Types.Regular && B.type == Ball.Types.Regular)
                { 
                    collisionType = 1;
                }

                if(A.type == Ball.Types.Regular && B.type == Ball.Types.Monster || (B.type == Ball.Types.Regular && A.type == Ball.Types.Monster))
                {
                    collisionType = 2;
                    if (B.type == Ball.Types.Regular && A.type == Ball.Types.Monster) (A, B) = (B, A);
                }

                if ((A.type == Ball.Types.Regular && B.type == Ball.Types.Repellent) || (B.type == Ball.Types.Regular && A.type == Ball.Types.Repellent))
                {
                    if (B.type == Ball.Types.Regular && A.type == Ball.Types.Repellent) (A, B) = (B, A);
                    collisionType = 3;
                }

                if(A.type == Ball.Types.Repellent && B.type==Ball.Types.Repellent)
                {
                    collisionType = 4;
                }

                if ((A.type == Ball.Types.Repellent && B.type == Ball.Types.Monster) || (B.type == Ball.Types.Repellent && A.type == Ball.Types.Monster))
                {
                    if (B.type == Ball.Types.Repellent && A.type == Ball.Types.Monster) (A, B) = (B, A);
                    collisionType = 5;
                }
                #endregion

                switch (collisionType)
                {
                    case 1:
                        if(A.Radius > B.Radius)
                        {
                            A.color = MyMath.CombineColorByWeights(A.color, A.Radius, B.color, B.Radius);
                            A.Radius += B.Radius;
                            objects.Remove(B);
                        }
                        else
                        {
                            B.color = MyMath.CombineColorByWeights(A.color, A.Radius, B.color, B.Radius);
                            B.Radius += A.Radius;
                            objects.Remove(A);
                        }
                        break;

                    case 2:
                        B.Radius += A.Radius;
                        objects.Remove(A);
                        break;
                    case 3:
                        B.color = A.color;
                        B.dx = -B.dx;
                        B.dy = -B.dy;
                        A.dx = -A.dx;
                        A.dy = -A.dy;
                        break;

                    case 4:
                        (A.color, B.color) = (B.color, A.color);
                        break;

                    case 5:
                        A.Radius /= 2;
                        A.dx = -A.dx;
                        A.dy = -A.dy;
                        break;
                }

            }

            foreach (Ball ball in objects)
            {

                ball.Tick();
            }
        }

        public void RenderObjects()
        {
            foreach(Ball b in objects)
            {
                b.Draw(graphicsHandler);
            }

        }

        public void RefreshScreen()
        {
            screen.Image = frame;
        }

        public void ClearScreen()
        {
            graphicsHandler.Clear(Color.White);
        }
        public void ClearScreen(Color c)
        {
            graphicsHandler.Clear(c);
        }
    }
}
