namespace TestEngine
{
    partial class Main
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.playerhealth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.encircleX = new System.Windows.Forms.NumericUpDown();
            this.encircleY = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.playerhealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.encircleX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.encircleY)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Teal;
            this.button1.Location = new System.Drawing.Point(397, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 45);
            this.button1.TabIndex = 4;
            this.button1.Text = "StartGame";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "PlayerHealth";
            // 
            // playerhealth
            // 
            this.playerhealth.Location = new System.Drawing.Point(95, 28);
            this.playerhealth.Name = "playerhealth";
            this.playerhealth.Size = new System.Drawing.Size(61, 21);
            this.playerhealth.TabIndex = 6;
            this.playerhealth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.playerhealth.ValueChanged += new System.EventHandler(this.playerhealth_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "encircle";
            // 
            // encircleX
            // 
            this.encircleX.Location = new System.Drawing.Point(71, 66);
            this.encircleX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.encircleX.Name = "encircleX";
            this.encircleX.Size = new System.Drawing.Size(50, 21);
            this.encircleX.TabIndex = 8;
            this.encircleX.Value = new decimal(new int[] {
            261,
            0,
            0,
            0});
            this.encircleX.ValueChanged += new System.EventHandler(this.encircleX_ValueChanged);
            // 
            // encircleY
            // 
            this.encircleY.Location = new System.Drawing.Point(138, 66);
            this.encircleY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.encircleY.Name = "encircleY";
            this.encircleY.Size = new System.Drawing.Size(44, 21);
            this.encircleY.TabIndex = 9;
            this.encircleY.Value = new decimal(new int[] {
            217,
            0,
            0,
            0});
            this.encircleY.ValueChanged += new System.EventHandler(this.encircleY_ValueChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(528, 313);
            this.Controls.Add(this.encircleY);
            this.Controls.Add(this.encircleX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.playerhealth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Name = "Main";
            this.Text = "Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.playerhealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.encircleX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.encircleY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown playerhealth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown encircleX;
        private System.Windows.Forms.NumericUpDown encircleY;
    }
}