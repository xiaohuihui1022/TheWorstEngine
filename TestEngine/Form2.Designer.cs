﻿namespace TestEngine
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.sans = new System.Windows.Forms.PictureBox();
            this.heart = new System.Windows.Forms.PictureBox();
            this.Line = new System.Windows.Forms.PictureBox();
            this.HealthCount = new System.Windows.Forms.Label();
            this.nowhealth = new System.Windows.Forms.Label();
            this.dead = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.sans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Line)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // sans
            // 
            this.sans.Location = new System.Drawing.Point(275, 20);
            this.sans.Name = "sans";
            this.sans.Size = new System.Drawing.Size(261, 191);
            this.sans.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.sans.TabIndex = 0;
            this.sans.TabStop = false;
            // 
            // heart
            // 
            this.heart.BackColor = System.Drawing.Color.Transparent;
            this.heart.Location = new System.Drawing.Point(489, 270);
            this.heart.Name = "heart";
            this.heart.Size = new System.Drawing.Size(16, 16);
            this.heart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.heart.TabIndex = 1;
            this.heart.TabStop = false;
            // 
            // Line
            // 
            this.Line.Image = ((System.Drawing.Image)(resources.GetObject("Line.Image")));
            this.Line.Location = new System.Drawing.Point(275, 230);
            this.Line.Name = "Line";
            this.Line.Size = new System.Drawing.Size(261, 217);
            this.Line.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Line.TabIndex = 3;
            this.Line.TabStop = false;
            // 
            // HealthCount
            // 
            this.HealthCount.AutoSize = true;
            this.HealthCount.ForeColor = System.Drawing.Color.Red;
            this.HealthCount.Location = new System.Drawing.Point(310, 497);
            this.HealthCount.Name = "HealthCount";
            this.HealthCount.Size = new System.Drawing.Size(78, 22);
            this.HealthCount.TabIndex = 5;
            this.HealthCount.Text = "100/100";
            // 
            // nowhealth
            // 
            this.nowhealth.AutoSize = true;
            this.nowhealth.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.nowhealth.Location = new System.Drawing.Point(12, 531);
            this.nowhealth.Name = "nowhealth";
            this.nowhealth.Size = new System.Drawing.Size(40, 22);
            this.nowhealth.TabIndex = 6;
            this.nowhealth.Text = "100";
            this.nowhealth.Visible = false;
            this.nowhealth.TextChanged += new System.EventHandler(this.nowhealth_TextChanged);
            // 
            // dead
            // 
            this.dead.AutoSize = true;
            this.dead.Font = new System.Drawing.Font("微软雅黑", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dead.ForeColor = System.Drawing.Color.Black;
            this.dead.Location = new System.Drawing.Point(151, 77);
            this.dead.Name = "dead";
            this.dead.Size = new System.Drawing.Size(519, 150);
            this.dead.TabIndex = 7;
            this.dead.Text = "U are DEAD!\r\npress X to restart\r\n";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(155, 497);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(149, 25);
            this.panel1.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(275, 230);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 76);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.heart);
            this.Controls.Add(this.Line);
            this.Controls.Add(this.sans);
            this.Controls.Add(this.HealthCount);
            this.Controls.Add(this.nowhealth);
            this.Controls.Add(this.dead);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "Form2";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.sans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Line)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox sans;
        private System.Windows.Forms.PictureBox heart;
        private System.Windows.Forms.PictureBox Line;
        private System.Windows.Forms.Label HealthCount;
        private System.Windows.Forms.Label nowhealth;
        private System.Windows.Forms.Label dead;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}