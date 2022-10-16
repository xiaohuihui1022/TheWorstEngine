using System;
using System.Drawing;
using System.Windows.Forms;
using TheWorstEngine;
using LogSystem;
using LangSystem;
using System.Threading;

namespace TestEngine
{
    public partial class Main : Form
    {
        Log log = new Log();
        Lang lang = new Lang();
        ConsoleGS con = new ConsoleGS();
        public Main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            // Thread RunTask = new Thread(Run);
            // RunTask.Start();
            log.LogWriteInit();
            log.TimeShowSet(true);
            con.LineX = decimal.ToInt32(encircleX.Value);
            con.LineY = decimal.ToInt32(encircleY.Value);
            con.playerHealth = playerhealth.Value.ToString();
            // log.Info(sans1.SetResolution(new Size(544, 351)));
            // log.Info(sans1.NewImage(@".\wt.gif"));
        }

        public void LangInit()
        {
            string[] LangType = { "简体中文", "English" };
            lang.Init();
            lang.SetLangType(LangType);
            lang.ChangeCreateLangType(0);
            lang.CreateLangStr("Null", "null");
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            // pictureBox1.Size = new Size(Width - 40, Height - 62);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(con);
            f2.Show();
        }

        #region Console相关events

        private void playerhealth_ValueChanged(object sender, EventArgs e)
        {
            con.playerHealth = playerhealth.Value.ToString();
        }

        private void encircleX_ValueChanged(object sender, EventArgs e)
        {
            con.LineX = decimal.ToInt32(encircleX.Value);
        }

        private void encircleY_ValueChanged(object sender, EventArgs e)
        {
            con.LineY = decimal.ToInt32(encircleY.Value);
        }

        #endregion

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // nothing have to do.
        }
    }
}
