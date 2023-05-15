using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace Balls
{
    public class Engine
    {
        private Graphics graphicsHandler;
        private PictureBox screen;
        private Bitmap frame;
        public List<Ball> balls;
        public Color backGroundColor { get; set; }

        public int ScreenWidth { get { return screen.Width; } }
        public int ScreenHeight { get { return screen.Height; } }

        public Engine(PictureBox screen)
        {
            balls = new List<Ball>();
            backGroundColor = Color.White;
            this.screen = screen;
            this.frame = new Bitmap(screen.Width, screen.Height);
            this.graphicsHandler = Graphics.FromImage(frame);
        }

        /// <summary>
        /// Random location
        /// </summary>
        public void AddBall(Ball.Types type)
        {
            Ball currentBall;
            bool ok;

            do
            {
                currentBall = new Ball(type);
                ok = true;
                foreach (Ball otherBall in balls)
                {
                    if(Physics.DiscreteCollision(currentBall, otherBall))
                    {
                        ok = false;
                        break;
                    }
                } 
            } while (!ok);
            
            balls.Add(currentBall);
        }

        /// <summary>
        /// Specific Location
        /// </summary>
        public void AddBall(Ball.Types type, int x, int y)
        {
            balls.Add(new Ball(type, x, y));
        }

        /// <summary>
        /// Removes all the balls from the scene
        /// </summary>
        public void DeleteBalls()
        {
            balls.Clear();
        }

        private class collisionPair
        {
            public int indexA, indexB;
            public bool fromRadiusChanging;
            public collisionPair(int indexA, int indexB)
            {
                this.indexA = Math.Min(indexA, indexB);
                this.indexB = Math.Max(indexB, indexA);
                this.fromRadiusChanging = false;
            }
            public Ball A, B;

        }

        /// <summary>
        /// Advances every ball
        /// </summary>
        public void TickBalls()
        {
            HandleWallCollisions();

            List<collisionPair> collisionPairs = DetermineCollidingBalls();

            HandleBallCollisions(collisionPairs);

            foreach (Ball ball in balls)
            {
                ball.Tick();
            }
        }

        private void HandleWallCollisions()
        {
            foreach (Ball currentBall in balls)
            {
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
            }
        }

        private void HandleBallCollisions(List<collisionPair> collisionPairs)
        {
            foreach (collisionPair cp in collisionPairs)
            {
                int collisionType = DetermineCollisionType(cp);

                Ball A = cp.A;
                Ball B = cp.B;

                switch (collisionType)
                {
                    case 1:
                        if (A.Radius > B.Radius)
                        {
                            A.color = MyMath.CombineColorsByWeights(A.color, A.Radius, B.color, B.Radius);
                            A.Radius += B.Radius;
                            balls.Remove(B);
                        }
                        else
                        {
                            B.color = MyMath.CombineColorsByWeights(A.color, A.Radius, B.color, B.Radius);
                            B.Radius += A.Radius;
                            balls.Remove(A);
                        }
                        break;

                    case 2:
                        B.Radius += A.Radius;
                        balls.Remove(A);
                        break;
                    case 3:
                        if (!cp.fromRadiusChanging)
                        {
                            B.color = A.color;
                            B.dx = -B.dx;
                            B.dy = -B.dy;
                            A.dx = -A.dx;
                            A.dy = -A.dy;
                        }
                        break;

                    case 4:
                        (A.color, B.color) = (B.color, A.color);
                        break;

                    case 5:
                        if (A.Radius <= 2)
                        {
                            balls.Remove(A);
                        }
                        else
                        {
                            A.Radius /= 2;
                            A.dx = -A.dx;
                            A.dy = -A.dy;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Regular   - Regular   - 1
        /// Regular   - Monster   - 2
        /// Regular   - Repellent - 3
        /// Repellent - Repellent - 4
        /// Repellent - Monster   - 5
        /// </summary>
        private int DetermineCollisionType(collisionPair cp)
        {
            Ball A = cp.A;
            Ball B = cp.B;

            int collisionType = 0;

            // regular - regular 1
            if (A.type == Ball.Types.Regular && B.type == Ball.Types.Regular)
            {
                collisionType = 1;
            }

            // regular - monster 2
            if ((A.type == Ball.Types.Regular && B.type == Ball.Types.Monster) || (B.type == Ball.Types.Regular && A.type == Ball.Types.Monster))
            {
                collisionType = 2;
                if (B.type == Ball.Types.Regular && A.type == Ball.Types.Monster) (cp.A, cp.B) = (cp.B, cp.A);
            }

            // regular repellent 3
            if ((A.type == Ball.Types.Regular && B.type == Ball.Types.Repellent) || (B.type == Ball.Types.Regular && A.type == Ball.Types.Repellent))
            {
                if (B.type == Ball.Types.Regular && A.type == Ball.Types.Repellent) (cp.A, cp.B) = (cp.B, cp.A);
                collisionType = 3;
            }

            // repellent - repellent 4
            if (A.type == Ball.Types.Repellent && B.type == Ball.Types.Repellent)
            {
                collisionType = 4;
            }

            // repellent - monster 5
            if ((A.type == Ball.Types.Repellent && B.type == Ball.Types.Monster) || (B.type == Ball.Types.Repellent && A.type == Ball.Types.Monster))
            {
                if (B.type == Ball.Types.Repellent && A.type == Ball.Types.Monster) (cp.A, cp.B) = (cp.B, cp.A);
                collisionType = 5;
            }

            return collisionType;
        }

        private List<collisionPair> DetermineCollidingBalls()
        {
            List<collisionPair> collisionPairs = new List<collisionPair>();
            List<collisionPair> nonCollisionPairs = new List<collisionPair>();

            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = 0; j < balls.Count; j++)
                {
                    if (i == j) continue;

                    collisionPair toTest = new collisionPair(Math.Min(i, j), Math.Max(i, j));
                    
                    if (collisionPairs.Find(x => x.indexA == toTest.indexA && x.indexB == toTest.indexB) != null) continue;

                    if (nonCollisionPairs.Find(x => x.indexA == toTest.indexA && x.indexB == toTest.indexB) != null) continue;

                    Ball currentBall = balls[i];
                    Ball otherBall = balls[j];

                    double y = Physics.ContinuousCollision(currentBall, otherBall);

                    if (0 < y && y <= 1)
                    {
                        toTest.A = balls[toTest.indexA];
                        toTest.B = balls[toTest.indexB];
                        collisionPairs.Add(toTest);
                    }
                    else
                    {
                        if(currentBall.radiusChanged || otherBall.radiusChanged)
                        {
                            if (Physics.DiscreteCollision(currentBall, otherBall))
                            {
                                toTest.A = balls[toTest.indexA];
                                toTest.B = balls[toTest.indexB];
                                toTest.fromRadiusChanging = true;
                                collisionPairs.Add(toTest);
                            }
                            else
                            {
                                nonCollisionPairs.Add(toTest);
                            }
                        }
                        else
                        {
                            nonCollisionPairs.Add(toTest);
                        }

                    }
                }
                balls[i].radiusChanged = false;
            }

            return collisionPairs;
        }

        /// <summary>
        /// Calculates the frame, but doesn't set it as display image
        /// </summary>
        public void RenderFrame()
        {
            foreach(Ball b in balls)
            {
                b.Draw(graphicsHandler, balls);
            }
        }

        /// <summary>
        /// Sets the current frame as the display image
        /// </summary>
        public void RefreshScreen()
        {
            screen.Image = frame;
        }

        /// <summary>
        /// Clears the screen using the current background color
        /// </summary>
        public void ClearScreen()
        {
            graphicsHandler.Clear(backGroundColor);
        }
    }
}
