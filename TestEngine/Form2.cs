using TheWorstEngine.AnimFunction;
using TheWorstEngine.UIFunction;
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
        private Thread Ending;
        public Form2(ConsoleGS con)
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            console = con;
            SansInit();
            HeartInit();
            HealthInit();
            mega.Load(@".\sound\mega.ogg");
            mega.SoundPlay();
        }
        private void HeartInit()
        {
            Heart.Load(this, heart);
            Heart.SetResolution(new Size(800, 600));
            Heart.SetImage(@".\img\red.png");
            Heart.Encircle(Line, heart, 4, 19);
            Heart.KeySet(Keys.Up, Keys.Down, Keys.Left, Keys.Right);
            Heart.CanMove(true);
        }
        private void SansInit()
        {
            sans1.Load(this, sans);
            string[] FS = { ".\\img\\sans1.png", ".\\img\\sans2.png",
                  ".\\img\\sans3.png", ".\\img\\sans4.png",
            ".\\img\\sans3.png", ".\\img\\sans2.png", ".\\img\\sans1.png"};
            sans1.SetAnim(FS, 5);
        }
        private void HealthInit()
        {
            Health.Load(this, HealthCount, nowhealth);
            Health.AttackCheck(sans, heart, 1, true, 5);
            Health.SetHurtSound(@".\sound\uts\hurt.wav");
            Ending = new Thread(EndThread);
            Ending.Start();
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            sans1.isClosed = true;
            mega.SoundStop();
            Ending.Abort();
        }
        private void EndThread()
        {
            while (true)
            {
                nowhealth.Text = console.playerHealth;
                Line.Size = new Size(console.LineX, console.LineY);
                if (nowhealth.Text == "0")
                {
                    mega.SoundStop();
                    dead.BackColor = Color.Red;
                    sans.Visible = false;
                    heart.Visible = false;
                    Line.Visible = false;
                    HealthCount.Visible = false;
                    EndingSound.Load(@".\sound\uts\afterdead.wav");
                    EndingSound.SoundPlay();
                    // TODO
                    Ending.Abort();
                }
                Thread.Sleep(50);
            }
        }
    }
}
