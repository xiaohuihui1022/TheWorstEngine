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
            this.sans = new System.Windows.Forms.PictureBox();
            this.heart = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heart)).BeginInit();
            this.SuspendLayout();
            // 
            // sans
            // 
            this.sans.Location = new System.Drawing.Point(118, 12);
            this.sans.Name = "sans";
            this.sans.Size = new System.Drawing.Size(324, 259);
            this.sans.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.sans.TabIndex = 0;
            this.sans.TabStop = false;
            // 
            // heart
            // 
            this.heart.Location = new System.Drawing.Point(229, 277);
            this.heart.Name = "heart";
            this.heart.Size = new System.Drawing.Size(23, 24);
            this.heart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.heart.TabIndex = 1;
            this.heart.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(457, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(528, 313);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.heart);
            this.Controls.Add(this.sans);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "Form2";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.sans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox sans;
        private System.Windows.Forms.PictureBox heart;
        private System.Windows.Forms.Label label1;
    }
}