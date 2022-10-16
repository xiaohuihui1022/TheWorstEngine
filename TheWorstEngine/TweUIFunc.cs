using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using IrrKlang;

namespace TheWorstEngine.UIFunction
{
    public class TweText
    {
        /* Global */

        // 窗口
        private Form form;
        // 输出文字的label(给用户看的)
        private Label HealthCount;
        // 输出文字给程序看的
        private Label NowHealth;
        // 玩家开始的血量
        private int PlayerInitHealth = 100;
        // 玩家当前血量
        private int PlayerNowHealth = 100;

        /* Attack */

        // 攻击者
        private PictureBox AtItem;
        // 被攻击者
        private PictureBox BeAtItem;
        // 被攻击后减少的血量
        private int ReduceHeal = 5;
        // 攻击检测线程
        private Thread BeAttackThread;
        // 是否有被攻击后的无敌时间
        private bool isInvicinbleTime;
        // 线程是否被关闭
        private bool isAttackThreadClosed = false;
        // 无敌时间长短(秒)
        private int InvicibleTime;

        /* sound */
        private readonly ISoundEngine SoundEngine = new ISoundEngine();
        private bool isHurtSound = false;
        private string HurtSound;

        /// <summary>
        /// 初始化加载函数
        /// </summary>
        /// <param name="f">窗口</param>
        /// <param name="l">要输出文字的label</param>
        public void Load(Form f, Label l, Label plyNowHealth)
        {
            form = f;
            HealthCount = l;
            NowHealth = plyNowHealth;
            PlayerInitHealth = int.Parse(plyNowHealth.Text);
            PlayerNowHealth = PlayerInitHealth;
            l.Text = plyNowHealth.Text + " / " + plyNowHealth.Text;
        }

        /// <summary>
        /// 攻击检测
        /// </summary>
        /// <param name="AttackItem">攻击者(如sans的骨头)</param>
        /// <param name="BeAttackItem">被攻击者(如决心)</param>
        /// <param name="ReduceHealth">被攻击所减少的血量</param>
        public void AttackCheck(PictureBox AttackItem, PictureBox BeAttackItem,
             int ReduceHealth)
        {
            AtItem = AttackItem;
            BeAtItem = BeAttackItem;
            ReduceHeal = ReduceHealth;
            BeAttackThread = new Thread(BeAttack);
            BeAttackThread.Start();
            form.FormClosing += new FormClosingEventHandler(Form_Closing);
        }

        /// <summary>
        /// 攻击检测
        /// </summary>
        /// <param name="AttackItem">攻击者(如sans的骨头)</param>
        /// <param name="BeAttackItem">被攻击者(如决心)</param>
        /// <param name="ReduceHealth">被攻击所减少的血量</param>
        /// <param name="Invicinble">无敌时间长短(毫秒)</param>
        public void AttackCheck(PictureBox AttackItem, PictureBox BeAttackItem,
             int ReduceHealth, int Invicinble)
        {
            isInvicinbleTime = true;
            InvicibleTime = Invicinble;
            AttackCheck(AttackItem, BeAttackItem, ReduceHeal);
        }

        /// <summary>
        /// 手动关闭线程，减轻电脑压力
        /// </summary>
        public void AttackCheckAbort()
        {
            BeAttackThread.Abort();
            isAttackThreadClosed = true;
        }

        /// <summary>
        /// 设置被攻击音效
        /// </summary>
        /// <param name="hurt">被攻击音效路径</param>
        public void SetHurtSound(string hurt)
        {
            HurtSound = hurt;
            isHurtSound = true;
        }
        // 关闭窗口自动终止线程
        private void Form_Closing(object sender,FormClosingEventArgs e)
        {
            if (!isAttackThreadClosed)
            {
                // 终止检测攻击线程
                BeAttackThread.Abort();
                isAttackThreadClosed = true;
            }
        }

        private void BeAttack()
        {
            while (true)
            {
                Rectangle pictureBox1ScreenBounds = AtItem.Bounds;
                Rectangle pictureBox2ScreenBounds = BeAtItem.Bounds;
                bool intersected = pictureBox2ScreenBounds.IntersectsWith(pictureBox1ScreenBounds);
                // 同步血量
                PlayerNowHealth = int.Parse(NowHealth.Text);
                if (intersected)
                {
                    // 血量减少
                    PlayerNowHealth -= ReduceHeal;
                    if (PlayerNowHealth <= 0)
                    {
                        PlayerNowHealth = 0;
                    }
                    // 被攻击
                    HealthCount.Text = PlayerNowHealth + "/" + PlayerInitHealth;
                    NowHealth.Text = PlayerNowHealth.ToString();
                    if (PlayerNowHealth == 0)
                    {
                        AttackCheckAbort();
                        return;
                    }
                    if (isHurtSound)
                    {
                        // 播放被攻击音效
                        SoundEngine.Play2D(HurtSound);
                    }
                    // 是否无敌时间
                    if (isInvicinbleTime)
                    {
                        Thread.Sleep(InvicibleTime);
                    }
                }
                Thread.Sleep(10);
                
            }
            
        }
    }
}
