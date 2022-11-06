using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheWorstEngine.AnimFunction;
using TheWorstEngine.UIFunction;
using System.Threading;

namespace TheWorstEngine.AttackCombo
{

    public class TweAttackCombo
    {
        // 当前窗口
        private Form form;

        // 单个图片
        private string SingleImg;

        // 攻击的东西
        private PictureBox AttackItem;

        // 被攻击者
        private PictureBox BeAtItem;

        // 判定框的pb
        private PictureBox Line;

        private Thread EnemyMoveThread;

        // 被攻击后减少的血量
        private int ReduceHeal = 5;

        // 是否有被攻击后的无敌时间
        private bool isInvicinbleTime = false;

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
        /// <param name="RealHealht">给程序看的healthlabel</param>
        public void Load(Form fo, PictureBox img, PictureBox square,Label HealthCountL,
            Label RealHealht)
        {
            form = fo;
            // SingleImg = img;
            Line = square;
            /*
            AttackItem = new PictureBox();
            AttackItem.SizeMode = PictureBoxSizeMode.CenterImage;
            AttackItem.Width = Width;
            AttackItem.Height = Height;
            AttackItem.Image = Image.FromFile(img);
            AttackItem.Location = new Point(x, y);
            form.Controls.Add(AttackItem);
            */
            AttackItem = img;
            AtCheck.Load(fo, HealthCountL, RealHealht);
        }

        /// <summary>
        /// 自动给每个img挂上AttackCheck(不带无敌时间)
        /// </summary>
        /// <param name="BeAttackItem">被攻击者(如决心)</param>
        /// <param name="ReduceHealth">被攻击所减少的血量</param>
        public void AutoAttackCheckSet(PictureBox BeAttackItem, int ReduceHealth)
        {
            BeAtItem = BeAttackItem;
            ReduceHeal = ReduceHealth;
            AtCheck.AttackCheck(AttackItem, BeAtItem, ReduceHeal);
        }

        /// <summary>
        /// 自动给每个img挂上AttackCheck(带无敌时间)
        /// </summary>
        /// <param name="BeAttackItem">被攻击者(如决心)</param>
        /// <param name="ReduceHealth">被攻击所减少的血量</param>
        /// <param name="Invicinble">无敌时间长短(毫秒)</param>
        public void AutoAttackCheckSet(PictureBox BeAttackItem,
             int ReduceHealth, int Invicinble, string HurtSound)
        {
            InvicibleTime = Invicinble;
            AutoAttackCheckSet(BeAttackItem, ReduceHeal);
            isInvicinbleTime = true;
            AtCheck.AttackCheck(AttackItem, BeAtItem, ReduceHeal, InvicibleTime);
            AtCheck.SetHurtSound(HurtSound);
        }

        /// <summary>
        /// 移动到的坐标
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
                int rx = r.Next(275, 498);
                int ry = r.Next(230, 404);
                Console.WriteLine("rx:" + rx + " ry" + ry);
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
                    else if (AttackItem.Location.X >= 498)
                    {
                        animationLib.Stop();
                        break;
                    }
                    else if (AttackItem.Location.Y >= 404)
                    {
                        animationLib.Stop();
                        break;
                    }
                    else if (AttackItem.Location.X <= 275)
                    {
                        animationLib.Stop();
                        break;
                    }
                    else if (AttackItem.Location.Y <= 230)
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
