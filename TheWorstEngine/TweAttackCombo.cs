using System;
using System.Drawing;
using System.Windows.Forms;
using TheWorstEngine.AnimFunction;
using TheWorstEngine.UIFunction;
using System.Threading;
using TheWorstEngine.Developer;

namespace TheWorstEngine.AttackCombo
{

    public class TweAttackCombo
    {
        // 当前窗口
        private Form form;

        // 攻击的东西
        private PictureBox AttackItem;

        // 被攻击者
        private PictureBox BeAtItem;

        // 判定框的pb
        private PictureBox linebox;

        // 判定panel
        private Panel IMGPanel;

        private Thread EnemyMoveThread;

        // 被攻击后减少的血量
        private int ReduceHeal = 5;

        // 无敌时间长短(秒)
        private int InvicibleTime;

        // 自制动画函数
        AnimationLib animationLib = new AnimationLib();

        // 自制的攻击检测函数
        TweText AtCheck = new TweText();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="img">单个图片路径</param>
        /// <param name="Valueget">ValueGet</param>
        /// <param name="Height">图片的高</param>
        /// <param name="Width">图片的宽</param>
        /// <param name="x">图片初始位置的x</param>
        /// <param name="y">图片初始位置的y</param>
        public void Load(ValueGet Valueget, string img, int Width, int Height, int x, int y)
        {
            form = Valueget.form;
            // SingleImg = img;
            linebox = Valueget.Line;
            AttackItem = new PictureBox();
            AttackItem.SizeMode = PictureBoxSizeMode.StretchImage;
            AttackItem.Width = Width;
            AttackItem.Height = Height;
            AttackItem.Image = Image.FromFile(img);
            AttackItem.Location = new Point(x, y);
            IMGPanel = Valueget.Renderer;
            // 异步调用
            if (IMGPanel.InvokeRequired)
            {
                IMGPanel.Invoke(new MethodInvoker(delegate
                {
                    IMGPanel.Controls.Add(AttackItem);
                }));
            }
            else
            {
                IMGPanel.Controls.Add(AttackItem);
            }
            linebox.SendToBack();
            AtCheck.Load(Valueget);
        }

        /// <summary>
        /// 自动给每个img挂上AttackCheck(带无敌时间)
        /// </summary>
        /// <param name="BeAttackItem">被攻击者(如决心)</param>
        /// <param name="ReduceHealth">被攻击所减少的血量</param>
        /// <param name="Invicinble">无敌时间长短(毫秒)</param>
        public void AutoAttackCheckSet(PictureBox BeAttackItem,
             int ReduceHealth, int Invicinble)
        {
            InvicibleTime = Invicinble;
            BeAtItem = BeAttackItem;
            ReduceHeal = ReduceHealth;
            AtCheck.AttackCheck(AttackItem, BeAtItem, ReduceHeal, InvicibleTime);
            
        }

        /// <summary>
        /// 移动到的坐标
        /// 建议向着四面八方移动，其他角度类型的移动我把握不住
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        public void MoveTo(int x, int y)
        {
            Point MoveTowards = new Point(x, y);
            animationLib.Load(AttackItem, 3, MoveTowards, form);
            animationLib.StartMove();
        }

        /* 测试代码
         
         
        /// <summary>
        /// 敌人随机移动
        /// </summary>
        public void EnemyRandomMove()
        {
            EnemyMoveThread = new Thread(EnemyMove);
            EnemyMoveThread.Start();
        }

        // 私有类
        private void EnemyMove()
        {
            Random r = new Random();
            form.FormClosing += new FormClosingEventHandler(Form_Closing);
            while (true)
            {
                int rx = r.Next(0, linebox.Size.Width - 5);
                int ry = r.Next(0, linebox.Size.Height - 5);
                MoveTo(rx, ry);
                // 是否移动完成
                while (true)
                {
                    Thread.Sleep(100);
                    if (animationLib.isMoveFinish)
                    {
                        animationLib.Stop();
                        break;
                    }
                    else if (AttackItem.Location.X >= linebox.Location.X)
                    {
                        animationLib.Stop();
                        break;
                    }
                    else if (AttackItem.Location.Y >= linebox.Location.Y)
                    {
                        animationLib.Stop();
                        break;
                    }
                    else if (AttackItem.Location.X <= 0)
                    {
                        animationLib.Stop();
                        break;
                    }
                    else if (AttackItem.Location.Y <= 0)
                    {
                        animationLib.Stop();
                        break;
                    }
                }
            }
        }*/

        public void EnemyDestroy()
        {
            IMGPanel.Controls.Remove(AttackItem);
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            EnemyMoveThread.Abort();
        }
    }
}
