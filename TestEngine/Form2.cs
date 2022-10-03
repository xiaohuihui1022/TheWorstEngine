using System;
using TheWorstEngine;
using System.Windows.Forms;

namespace TestEngine
{
    public partial class Form2 : Form
    {
        Twe sans1 = new Twe();
        Twe Heart = new Twe();
        public Form2()
        {
            InitializeComponent();
            sans1.form = this;
            sans1.picturebox = sans;
            string[] FS = { ".\\img\\sans1.png", ".\\img\\sans2.png",
                ".\\img\\sans3.png", ".\\img\\sans4.png",
            ".\\img\\sans3.png", ".\\img\\sans2.png", ".\\img\\sans1.png"};
            sans1.NewAnim(FS, 5, label1);
            Heart.form = this;
            Heart.picturebox = heart;
            Heart.NewImage(@".\img\red.png");
            // Heart.KeySet(Keys.Up, Keys.Down, Keys.Left, Keys.Right);
            Heart.CanMove(true);

            // sans1.KeySet(Keys.Up, Keys.Down, Keys.Left, Keys.Right);
            // sans1.CanMove(true);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            sans1.isClosed = true;
        }
    }
}
