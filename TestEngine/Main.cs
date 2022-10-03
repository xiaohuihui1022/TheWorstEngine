using System;
using System.Drawing;
using System.Windows.Forms;
using TheWorstEngine;
using LogSystem;
using LangSystem;

namespace TestEngine
{
    public partial class Main : Form
    {
        Twe sans1 = new Twe();
        Twe sans2 = new Twe();
        Log log = new Log();
        Lang lang = new Lang();
        public Main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //Thread RunTask = new Thread(Run);
            //RunTask.Start();
            sans2.form = this;
            sans2.picturebox = pictureBox2;
            sans1.form = this;
            sans1.picturebox = pictureBox1;
            log.LogWriteInit();
            log.TimeShowSet(true);
            log.Info(sans1.SetResolution(new Size(544, 351)));
            // log.Info(sans1.NewImage(@".\wt.gif"));

            string[] FS = { ".\\img\\sans1.png", ".\\img\\sans2.png",
                ".\\img\\sans3.png", ".\\img\\sans4.png",
            ".\\img\\sans3.png", ".\\img\\sans2.png", ".\\img\\sans1.png"};
            // pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            sans1.NewAnim(FS, 5, FPS);
            sans2.NewAnim(FS, 5, FPS2);
        }

        public void LangInit()
        {
            string[] LangType = { "简体中文", "English" };
            lang.Init();
            lang.SetLangType(LangType);
            lang.ChangeCreateLangType(0);
            lang.CreateLangStr("Null", "null");
        }
        /// <summary>
        /// 负责游戏刷新的函数，可重写
        /// </summary>
        //public static void Run()
        //{
        //    Thread.Sleep(10000);
        //}


        private void Main_SizeChanged(object sender, EventArgs e)
        {
            // pictureBox1.Size = new Size(Width - 40, Height - 62);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            sans1.isClosed = true;
            sans2.isClosed = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}
