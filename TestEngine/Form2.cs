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
            SansInit();
            HeartInit();
            mega.Load(@".\sound\mega.ogg");
            mega.SoundPlay();
        }
        private void HeartInit()
        {
            Heart.Load(this, heart);
            Heart.SetResolution(new System.Drawing.Size(800, 600));
            Heart.SetImage(@".\img\red.png");
            Heart.Encircle(Line, heart);
            Heart.KeySet(Keys.Up, Keys.Down, Keys.Left, Keys.Right);
            Heart.CanMove(true);
        }
        private void SansInit()
        {
            sans1.Load(this, sans);
            string[] FS = { ".\\img\\sans1.png", ".\\img\\sans2.png",
                  ".\\img\\sans3.png", ".\\img\\sans4.png",
            ".\\img\\sans3.png", ".\\img\\sans2.png", ".\\img\\sans1.png"};
            sans1.SetAnim(FS, 5, label1);
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            sans1.isClosed = true;
            mega.SoundStop();
        }
    }
}
