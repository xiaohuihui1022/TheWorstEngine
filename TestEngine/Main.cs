using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheWorstEngine;
using LogSystem;

namespace TestEngine
{
    public partial class Main : Form
    {
        Twe twe = new Twe();
        Log log = new Log();

        public Main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //Thread RunTask = new Thread(Run);
            //RunTask.Start();
            twe.form = this;
            twe.picturebox = pictureBox1;
            log.LogWriteInit();
            log.TimeShowSet(true);
            log.Info(twe.SetResolution(new Size(800, 600)));
            // log.Info(twe.NewImage(@".\wt.gif"));

            string[] FS = { ".\\img\\sans1.png", ".\\img\\sans2.png", ".\\img\\sans3.png", ".\\img\\sans4.png" };
            // pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            twe.NewAnim(FS, 114, FPS);
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
            pictureBox1.Size = new Size(Width - 40, Height - 62);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            twe.isClosed = true;
        }
    }
}
