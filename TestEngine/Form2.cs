using TheWorstEngine.AnimFunction;
using TheWorstEngine.UIFunction;
using TheWorstEngine.AttackCombo;
using System.Windows.Forms;
using System.Threading;
using System;
using System.Drawing;
using TheWorstEngine.Developer;

namespace TestEngine
{
    public partial class Form2 : Form
    {
        TweAnim sans1 = new TweAnim();
        TweAnim Heart = new TweAnim();
        TweSound mega = new TweSound();
        TweText Health = new TweText();
        TweSound EndingSound = new TweSound();
        UTProgress utp = new UTProgress();
        TweAttackCombo enemy = new TweAttackCombo();
        Scripter scripter = new Scripter();

        // 处理数据
        ValueGet Valueget = new ValueGet();
        public Form2()
        {
            utp.Value = 100;
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            // 初始化各种全局数值
            ValueInit();
            // 初始化各种Item
            SansInit();
            HeartInit();
            HealthInit();
            ProgressInit();
            // 加载音乐
            mega.Load(@".\sound\yijibattle.ogg");
            mega.SoundPlay();
            scripter.LoadScript(".\\scripts\\example.script");
            // enemy.Load(Valueget, @".\img\gb\GB1.png", 50, 50, 10, 10);
            // enemy.AutoAttackCheckSet(heart, 5, 1);

        }
        private void HeartInit()
        {
            Heart.Load(Valueget, heart);
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
            sans1.Load(Valueget, sans);
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
            Health.Load(Valueget);
            // Health.AttackCheck(sans, heart, 1, 100); // 1！ 5！(前者为攻击锁掉血量，后者为无敌时间(ms))
            Health.SetHurtSound(@".\sound\uts\hurt.wav");
        }
        private void ValueInit()
        {
            Valueget.GlobalHurtSound = @".\sound\uts\hurt.wav";
            Valueget.HealthCount = this.HealthCount;
            Valueget.form = this;
            Valueget.Renderer = this.panel2;
            Valueget.Line = this.Line;
            Valueget.SansPicture = this.sans;
            Valueget.HeartPicture = this.heart;
            // 设置Scripter
            scripter.Valueget = Valueget;
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
        // Restart Game
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X)
            {
                EndingSound.SoundStop();
                utp.Value = 100;
                Valueget.PlayerHealth = 100;
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
                scripter.LoadScript(".\\scripts\\example.script");
                this.KeyDown -= OnKeyDown;
            }
        }

        private void HealthCount_TextChanged(object sender, EventArgs e)
        {
            int PlayerHealth = Valueget.PlayerHealth;
            utp.Value = PlayerHealth;
            utp.Width = utp.Width * (utp.Value / utp.Maximum);
            if (PlayerHealth <= 0)
            {
                Heart.CanMove(false);
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
            }
        }
    }
}
