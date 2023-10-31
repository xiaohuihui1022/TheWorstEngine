﻿using TheWorstEngine.AnimFunction;
using TheWorstEngine.UIFunction;
using TheWorstEngine.AttackCombo;
using System.Windows.Forms;
using System.Threading;
using System;
using System.Drawing;

namespace TestEngine
{
    public partial class Form2 : Form
    {
        TweAnim sans1 = new TweAnim();
        TweAnim Heart = new TweAnim();
        TweSound mega = new TweSound();
        TweText Health = new TweText();
        TweSound EndingSound = new TweSound();
        ConsoleGS console;
        UTProgress utp = new UTProgress();
        TweAttackCombo enemy = new TweAttackCombo();
        private Thread Ending;
        public Form2(ConsoleGS con)
        {
            utp.Value = 100;
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            console = con;
            SansInit();
            HeartInit();
            HealthInit();
            ProgressInit();
            mega.Load(@".\sound\yijibattle.ogg");
            mega.SoundPlay();
            enemy.Load(this, @".\img\gb\GB1.png", Line, // 换行
            HealthCount, panel2, nowhealth, 50, 50, 10, 10);
            enemy.SetGlobalHurtSound(@".\sound\uts\hurt.wav");
            enemy.AutoAttackCheckSet(heart, 5, 1);
            // enemy.EnemyRandomMove();
        }
        private void HeartInit()
        {
            Heart.Load(this, heart);
            Heart.SetResolution(new Size(800, 600));
            // Heart.SetImage(@".\img\red.png");
            Heart.Encircle(Line, heart, 4, 19);
            Heart.KeySet(Keys.Up, Keys.Down, Keys.Left, Keys.Right);
            Heart.CanMove(true);
        }
        private void SansInit()
        {
             string[] FS = { ".\\img\\sans1.png", ".\\img\\sans2.png",
                  ".\\img\\sans3.png", ".\\img\\sans4.png",
            ".\\img\\sans3.png", ".\\img\\sans2.png", ".\\img\\sans1.png"};
            sans1.SetAnim(FS, 5);
            sans1.Load(this, sans);
            // sans1.SetImage(@".\img\Froggit.png");
        }
        private void ProgressInit()
        {
            panel1.Controls.Add(utp);
            utp.Dock = DockStyle.Fill;
            utp.Minimum = 0;
            utp.Maximum = 100;
            // 防止闪烁现象 
            Label l = new Label();
            l.Parent = utp;
            l.BackColor = Color.Transparent;
            l.ForeColor = Color.Red;
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.Width = utp.Width;
            l.Height = utp.Height;
        }
        private void HealthInit()
        {
            Health.Load(this, HealthCount, nowhealth);
            Health.AttackCheck(sans, heart, 1, 100); // 1！ 5！(前者为攻击锁掉血量，后者为无敌时间(ms))
            Health.SetHurtSound(@".\sound\uts\hurt.wav");
            // Ending = new Thread(EndThread);
            // Ending.Start();
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
        /*
        private void EndThread()
        {
            while (true)
            {
                // 锁血
                // nowhealth.Text = console.playerHealth;
                panel2.Size = new Size(console.LineX, console.LineY);
                Thread.Sleep(50);
            }
        }*/

        private void nowhealth_TextChanged(object sender, EventArgs e)
        {
            utp.Value = int.Parse(nowhealth.Text);
            utp.Width = utp.Width * (utp.Value / utp.Maximum);
            if (nowhealth.Text == "0")
            {
                mega.SoundPaused();
                dead.BackColor = Color.Red;
                dead.Visible = true;
                sans.Visible = false;
                heart.Visible = false;
                Line.Visible = false;
                HealthCount.Visible = false;
                panel1.Visible = false;
                EndingSound.Load(@".\sound\uts\afterdead.wav");
                EndingSound.SoundPlay();
                this.KeyDown += OnKeyDown;
                // TODO
                // Ending.Abort();
            }
        }
        // Restart Game
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X)
            {
                EndingSound.SoundStop();
                utp.Value = 100;
                nowhealth.Text = "100";
                SansInit();
                HeartInit();
                HealthInit();
                ProgressInit();
                mega.SoundDispaused();
                sans.Visible = true;
                heart.Visible = true;
                Line.Visible = true;
                HealthCount.Visible = true;
                panel1.Visible = true;
                dead.Visible = false;
                enemy.AutoAttackCheckSet(heart, 5, 1);
            }
        }
    }
}
