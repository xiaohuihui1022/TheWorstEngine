using System;
using TheWorstEngine.AnimFunction;
using System.Windows.Forms;

namespace TestEngine
{
    public partial class Form2 : Form
    {
        TweAnim sans1 = new TweAnim();
        TweAnim Heart = new TweAnim();
        TweSound mega = new TweSound();
        public Form2()
        {
            InitializeComponent();
            sans1.Load(this, sans);
            mega.Load(this, @".\sound\mega.ogg");
            string[] FS = { ".\\img\\sans1.png", ".\\img\\sans2.png",
                ".\\img\\sans3.png", ".\\img\\sans4.png",
            ".\\img\\sans3.png", ".\\img\\sans2.png", ".\\img\\sans1.png"};
            sans1.SetAnim(FS, 5, label1);
            Heart.Load(this, heart);
            Heart.SetImage(@".\img\red.png");
            // Heart.KeySet(Keys.Up, Keys.Down, Keys.Left, Keys.Right);
            Heart.CanMove(true);
            mega.PlaySound();
            // sans1.AnimSound(@".\sound\mega.ogg");
            // sans1.KeySet(Keys.Up, Keys.Down, Keys.Left, Keys.Right);
            // sans1.CanMove(true);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            sans1.isClosed = true;
        }
    }
}
