using System.Drawing;
using System.Windows.Forms;

namespace TheWorstEngine.UIFunction
{
    public partial class UTProgress : ProgressBar
    {
        public UTProgress()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            SolidBrush brush = null;
            Rectangle bounds = new Rectangle(0, 0, base.Width, base.Height);
            //...
            //e.Graphics.FillRectangle(new SolidBrush(this.BackColor), 1, 1, bounds.Width, bounds.Height);
            // bounds.Height -= 4;
            bounds.Width = ((int)(bounds.Width * (((double)base.Value) / ((double)base.Maximum))));
            brush = new SolidBrush(Color.Yellow);
            pe.Graphics.FillRectangle(brush, 0, 0, bounds.Width, bounds.Height);
        }
    }
}
