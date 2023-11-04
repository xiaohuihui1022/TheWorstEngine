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
        /// <param name="fo">当前窗口(填this即可)</param>
        /// <param name="img">单个图片路径</param>
        /// <param name="square">活动框的picturebox</param>
        /// <param name="HealthCountL">显示血量的label</param>
        /// <param name="imgpanel">图床所用的panel</param>
        /// <param name="HealthGetSet">给程序看的healthlabel</param>
        /// <param name="Height">图片的高</param>
        /// <param name="Width">图片的宽</param>
        /// <param name="x">图片初始位置的x</param>
        /// <param name="y">图片初始位置的y</param>
        public void Load(Form fo, string img, PictureBox square,Label HealthCountL, Panel imgpanel,
            ValueGet Valueget, int Width, int Height, int x, int y)
        {
            form = fo;
            // SingleImg = img;
            linebox = square;
            AttackItem = new PictureBox();
            AttackItem.SizeMode = PictureBoxSizeMode.StretchImage;
            AttackItem.Width = Width;
            AttackItem.Height = Height;
            AttackItem.Image = Image.FromFile(img);
            AttackItem.Location = new Point(x, y);
            imgpanel.Controls.Add(AttackItem);
            square.SendToBack();
            AtCheck.Load(fo, HealthCountL, Valueget);
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
        /// 设置全局hurtsound（一个项目只用设置一次，全局应用）
        /// </summary>
        /// <param name="Hurtsound">受伤音效位置</param>
        public void SetGlobalHurtSound(string Hurtsound) => AtCheck.SetHurtSound(Hurtsound);

        /// <summary>
        /// 移动到的坐标
        /// 建议向着四面八方移动，其他角度类型的移动我把握不住
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        public void MoveTo(int x, int y)
        {
            Point MoveTowards = new Point(x, y);
            animationLib.Load(AttackItem, 5, MoveTowards, form);
            animationLib.Start();
        }

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
        }
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            EnemyMoveThread.Abort();
        }
    }
}
