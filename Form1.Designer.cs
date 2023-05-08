namespace Balls
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toggle_Button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.FPSTextBox = new System.Windows.Forms.TextBox();
            this.random_Button = new System.Windows.Forms.Button();
            this.step_Button = new System.Windows.Forms.Button();
            this.spawn_Regular_Button = new System.Windows.Forms.Button();
            this.spawn_Repellent_Button = new System.Windows.Forms.Button();
            this.spawn_Monster_Button = new System.Windows.Forms.Button();
            this.clear_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1075, 863);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // toggle_Button
            // 
            this.toggle_Button.BackColor = System.Drawing.SystemColors.Control;
            this.toggle_Button.Location = new System.Drawing.Point(1106, 125);
            this.toggle_Button.Name = "toggle_Button";
            this.toggle_Button.Size = new System.Drawing.Size(145, 177);
            this.toggle_Button.TabIndex = 1;
            this.toggle_Button.Text = "Toggle";
            this.toggle_Button.UseVisualStyleBackColor = false;
            this.toggle_Button.Click += new System.EventHandler(this.toggle_Button_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FPSTextBox
            // 
            this.FPSTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FPSTextBox.Location = new System.Drawing.Point(1106, 12);
            this.FPSTextBox.Multiline = true;
            this.FPSTextBox.Name = "FPSTextBox";
            this.FPSTextBox.Size = new System.Drawing.Size(296, 70);
            this.FPSTextBox.TabIndex = 2;
            // 
            // random_Button
            // 
            this.random_Button.Location = new System.Drawing.Point(1106, 340);
            this.random_Button.Name = "random_Button";
            this.random_Button.Size = new System.Drawing.Size(180, 52);
            this.random_Button.TabIndex = 3;
            this.random_Button.Text = "Generate Random";
            this.random_Button.UseVisualStyleBackColor = true;
            this.random_Button.Click += new System.EventHandler(this.random_Button_Click);
            // 
            // step_Button
            // 
            this.step_Button.Location = new System.Drawing.Point(1257, 125);
            this.step_Button.Name = "step_Button";
            this.step_Button.Size = new System.Drawing.Size(145, 177);
            this.step_Button.TabIndex = 4;
            this.step_Button.Text = "Step";
            this.step_Button.UseVisualStyleBackColor = true;
            this.step_Button.Click += new System.EventHandler(this.step_Button_Click);
            // 
            // spawn_Regular_Button
            // 
            this.spawn_Regular_Button.Location = new System.Drawing.Point(1106, 428);
            this.spawn_Regular_Button.Name = "spawn_Regular_Button";
            this.spawn_Regular_Button.Size = new System.Drawing.Size(180, 52);
            this.spawn_Regular_Button.TabIndex = 5;
            this.spawn_Regular_Button.Text = "Spawn Regular Ball";
            this.spawn_Regular_Button.UseVisualStyleBackColor = true;
            this.spawn_Regular_Button.Click += new System.EventHandler(this.spawn_Regular_Button_Click);
            // 
            // spawn_Repellent_Button
            // 
            this.spawn_Repellent_Button.Location = new System.Drawing.Point(1106, 512);
            this.spawn_Repellent_Button.Name = "spawn_Repellent_Button";
            this.spawn_Repellent_Button.Size = new System.Drawing.Size(180, 52);
            this.spawn_Repellent_Button.TabIndex = 6;
            this.spawn_Repellent_Button.Text = "Spawn Repellent Ball";
            this.spawn_Repellent_Button.UseVisualStyleBackColor = true;
            this.spawn_Repellent_Button.Click += new System.EventHandler(this.spawn_Repellent_Button_Click);
            // 
            // spawn_Monster_Button
            // 
            this.spawn_Monster_Button.Location = new System.Drawing.Point(1106, 591);
            this.spawn_Monster_Button.Name = "spawn_Monster_Button";
            this.spawn_Monster_Button.Size = new System.Drawing.Size(180, 52);
            this.spawn_Monster_Button.TabIndex = 7;
            this.spawn_Monster_Button.Text = "Spawn Monster Ball";
            this.spawn_Monster_Button.UseVisualStyleBackColor = true;
            this.spawn_Monster_Button.Click += new System.EventHandler(this.spawn_Monster_Button_Click);
            // 
            // clear_Button
            // 
            this.clear_Button.Location = new System.Drawing.Point(1313, 340);
            this.clear_Button.Name = "clear_Button";
            this.clear_Button.Size = new System.Drawing.Size(82, 140);
            this.clear_Button.TabIndex = 8;
            this.clear_Button.Text = "Clear";
            this.clear_Button.UseVisualStyleBackColor = true;
            this.clear_Button.Click += new System.EventHandler(this.clear_Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 939);
            this.Controls.Add(this.clear_Button);
            this.Controls.Add(this.spawn_Monster_Button);
            this.Controls.Add(this.spawn_Repellent_Button);
            this.Controls.Add(this.spawn_Regular_Button);
            this.Controls.Add(this.step_Button);
            this.Controls.Add(this.random_Button);
            this.Controls.Add(this.FPSTextBox);
            this.Controls.Add(this.toggle_Button);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button toggle_Button;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox FPSTextBox;
        private System.Windows.Forms.Button random_Button;
        private System.Windows.Forms.Button step_Button;
        private System.Windows.Forms.Button spawn_Regular_Button;
        private System.Windows.Forms.Button spawn_Repellent_Button;
        private System.Windows.Forms.Button spawn_Monster_Button;
        private System.Windows.Forms.Button clear_Button;
    }
}

