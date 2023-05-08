using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace Balls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Engine engine;
        Stopwatch framerateTester = new Stopwatch();

        private void Form1_Load(object sender, EventArgs e)
        {
            toggle_Button.BackColor = Color.IndianRed;
            spawn_Regular_Button.BackColor = Color.LightGray;
            spawn_Repellent_Button.BackColor = Color.LightGray;
            spawn_Monster_Button.BackColor = Color.LightGray;

            timer1.Enabled = false;
            timer1.Interval = 1;

            pictureBox1.BackColor = Color.White;
            engine = new Engine(pictureBox1);
        }
        
        private void toggle_Button_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            if(timer1.Enabled)
                toggle_Button.BackColor = Color.LightGreen;
            else
                toggle_Button.BackColor = Color.IndianRed;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            framerateTester.Start();

            engine.ClearScreen();
            engine.RenderObjects();
            engine.RefreshScreen();
            engine.TickBalls();
            framerateTester.Stop();
            double ms = (double)Math.Max(framerateTester.ElapsedMilliseconds, 1);
            double fps = 1000 / (ms * timer1.Interval);
            FPSTextBox.Text = "FPS:" + Environment.NewLine + fps.ToString("#.##");
            framerateTester.Reset();
        }

        private void random_Button_Click(object sender, EventArgs e)
        {
            engine.DeleteBalls();

            for (int i = 0; i < 3; i++) engine.AddBall(Ball.Types.Repellent);

            for (int i = 0; i < 35; i++) engine.AddBall(Ball.Types.Regular);

            engine.AddBall(Ball.Types.Monster);

            engine.ClearScreen();
            engine.RenderObjects();
            engine.RefreshScreen();
        }

        private void step_Button_Click(object sender, EventArgs e)
        {
            FPSTextBox.Text = "FPS:" + Environment.NewLine + "-";
            timer1.Enabled = false;
            engine.ClearScreen();
            engine.TickBalls();
            engine.RenderObjects();
            engine.RefreshScreen();
        }

        bool spawnRegular = false;
        bool spawnMonster = false;
        bool spawnRepellent = false;
        int spawnType = 0;
        private void spawn_Regular_Button_Click(object sender, EventArgs e)
        {
            spawnRegular = !spawnRegular;

            if(spawnRegular)
            {
                spawnRepellent = false;
                spawnMonster = false;

                spawnType = 1;

                spawn_Regular_Button.BackColor = Color.RoyalBlue;
                spawn_Repellent_Button.BackColor = Color.LightGray;
                spawn_Monster_Button.BackColor = Color.LightGray;
            }
            else
            {
                spawn_Regular_Button.BackColor = Color.LightGray;
                spawnType = 0;
            }
        }

        private void spawn_Repellent_Button_Click(object sender, EventArgs e)
        {
            spawnRepellent = !spawnRepellent;

            if (spawnRepellent)
            {
                spawnRegular = false;
                spawnMonster = false;

                spawnType = 2;

                spawn_Repellent_Button.BackColor = Color.RoyalBlue;
                spawn_Regular_Button.BackColor = Color.LightGray;
                spawn_Monster_Button.BackColor = Color.LightGray;
            }
            else
            {
                spawn_Repellent_Button.BackColor = Color.LightGray;
                spawnType = 0;
            }
        }

        private void spawn_Monster_Button_Click(object sender, EventArgs e)
        {
            spawnMonster = !spawnMonster;

            if (spawnMonster)
            {
                spawnRegular = false;
                spawnRepellent = false;

                spawnType = 3;

                spawn_Monster_Button.BackColor = Color.RoyalBlue;
                spawn_Regular_Button.BackColor = Color.LightGray;
                spawn_Repellent_Button.BackColor = Color.LightGray;
            }
            else
            {
                spawn_Monster_Button.BackColor = Color.LightGray;
                spawnType = 0;
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (spawnType)
            {
                case 1:
                    engine.AddBall(new Ball(Ball.Types.Regular, e.X, e.Y, 0, 0, engine.screen.Width, engine.screen.Height));
                    break;
                case 2:
                    engine.AddBall(new Ball(Ball.Types.Repellent, e.X, e.Y, 0, 0, engine.screen.Width, engine.screen.Height));
                    break;
                case 3:
                    engine.AddBall(new Ball(Ball.Types.Monster, e.X, e.Y, 0, 0, engine.screen.Width, engine.screen.Height));
                    break;
            }

            engine.RenderObjects();
            engine.RefreshScreen();
        }

        private void clear_Button_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            toggle_Button.BackColor = Color.IndianRed;

            engine.DeleteBalls();
            engine.ClearScreen();
            engine.RefreshScreen();
        }

    }
}
