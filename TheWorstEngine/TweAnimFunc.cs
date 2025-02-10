using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

// 音频处理
using IrrKlang;
using TheWorstEngine.Developer;
// KeyManager
// using System.Runtime.InteropServices;   //调用WINDOWS API函数时要用到
// using Microsoft.Win32;  //写入注册表时要用到

namespace TheWorstEngine.AnimFunction
{
    public class TweAnim
    {
        /// <summary>
        /// 添加窗口
        /// </summary>
        private Form form;
        /// <summary>
        /// 添加图片box
        /// </summary>
        private PictureBox picturebox;
        /// <summary>
        /// 检测程序是否被关闭
        /// </summary>
        public bool isClosed = false;
        

        /* MoveEvent */

        // 向上的按键
        private Keys UpKeyGlobal = Keys.W;
        // 向下的按键
        private Keys DownKeyGlobal = Keys.S;
        // 向左的按键
        private Keys LeftKeyGlobal = Keys.A;
        // 向右的按键
        private Keys RightKeyGlobal = Keys.D;
        // 移动速度
        private int MoveSpeed = 5;

        /* AnimEvent */

        // 动画图片
        private string[] FilesNameGlobal = { };
        // 动画FPS
        private int FPSGlobal = 0;
        // FPS显示
        private Label FPSLabelG = new Label();
        // 是否显示FPS
        private bool isFPSShow = false;

        /* EnCircle */

        // 北面和西面的碰撞
        private int NandW = 10;
        // 南面和东面的碰撞
        private int SandE = 32;
        // 在FormClosing和move函数间的桥（是否可移动）
        private bool CanMoveOut = false;
        // 在FormClosing和EnCircleThread函数间的桥（是否可移动）
        private bool CanEnCircle = false;
        // 在FormClosing和move函数间的桥（移动thread）
        private Thread mthread;
        // 在FormClosing和EnCircleThread的桥
        private Thread Ethread;
        // 包围(相当于UT的白框)
        private PictureBox Encirclement;
        // 被包围(相当于UT的决心)
        private PictureBox BeEncirclement;
        public void Load(ValueGet valueGet, PictureBox image)
        {
            form = valueGet.form;
            picturebox = image;
        }
        /// <summary>
        /// 设置窗口分辨率
        /// </summary>
        /// <param name="size">new System.Drawing.Size(1280, 720)</param>
        public string SetResolution(Size size)
        {
            form.Size = size;
            return "已将分辨率设为:" + size;
        }
        // 帧动画/图片函数
        /// <summary>
        /// 帧动画
        /// </summary>
        /// <param name="FilesName">文件集,将所有需要参与演算的图片路径存到这个数组里</param>
        /// <param name="FPS">帧数</param>
        /// <returns></returns>
        public string SetAnim(string[] FilesName, int FPS)
        {
            FilesNameGlobal = FilesName;
            FPSGlobal = FPS;
            Thread Anim = new Thread(AnimFor);
            Anim.Start();
            return "动画已创建";
        }
        /// <summary>
        /// 设置图片用的
        /// </summary>
        /// <param name="FileName">图片路径，可以为任何图片格式(包括gif)</param>
        /// <returns></returns>
        public string SetImage(string FileName)
        {
            picturebox.Image = Image.FromFile(FileName);
            return "已将图片设置为:" + FileName;
        }
        // 移动函数
        /// <summary>
        /// 是否可以移动
        /// </summary>
        /// <param name="CanItMove">true则可以移动，false则为贴图</param>
        public void CanMove(bool CanItMove)
        {
            if (CanItMove)
            {
                CanMoveOut = CanItMove;
                /*
                KeyManager k_hook = new KeyManager();
                k_hook.KeyDownEvent += new KeyEventHandler(Hook_KeyDown);//钩住键按下
                k_hook.KeyUpEvent += new KeyEventHandler(Hook_KeyUp);
                k_hook.Start();//安装键盘钩子*/
                // 测试函数
                mthread = new Thread(MoveThread);
                mthread.Start();
                form.KeyDown += new KeyEventHandler(Hook_KeyDown);
                form.KeyUp += new KeyEventHandler(Hook_KeyUp);
                form.FormClosing += new FormClosingEventHandler(Form_Closing);
            }
        }
        /// <summary>
        /// 是否可以移动
        /// </summary>
        /// <param name="CanItMove">true则可以移动, false则为贴图</param>
        /// <param name="MS">MoveSpeed,即移动速度,默认5</param>
        public void CanMove(bool CanItMove, int MS)
        {
            MoveSpeed = MS;
            CanMove(CanItMove);
        }
        /// <summary>
        /// 设置移动速度
        /// </summary>
        /// <param name="MS">移动速度(默认为5)</param>
        public void SetMoveSpeed(int MS)
        {
            MoveSpeed = MS;
        }
        /// <summary>
        /// picturebox把picturebox包围
        /// </summary>
        /// <param name="Encircled">包围(相当于白框)</param>
        /// <param name="BeEncircled">被包围(相当于决心)</param>
        /// <param name="NorthWest">北面和西面的碰撞间隔</param>
        /// <param name="SouthEast">南面和东面的碰撞间隔</param>
        public void Encircle(PictureBox Encircled, PictureBox BeEncircled, int NorthWest, int SouthEast)
        {
            Encirclement = Encircled;
            BeEncirclement = BeEncircled;
            NandW = NorthWest;
            SandE = SouthEast;
            Ethread = new Thread(EnCircleThread);
            CanEnCircle = true;
            Ethread.Start();
            form.FormClosing += new FormClosingEventHandler(Form_Closing);
        }
        
        /// <summary>
        /// 设置移动按键
        /// </summary>
        /// <param name="UpKey">向上走按键</param>
        /// <param name="DownKey">向下走按键</param>
        /// <param name="LeftKey">向左走按键</param>
        /// <param name="RightKey">向右走按键</param>
        /// <returns></returns>
        public string KeySet(Keys UpKey, Keys DownKey, Keys LeftKey, Keys RightKey)
        {
            UpKeyGlobal = UpKey;
            DownKeyGlobal = DownKey;
            LeftKeyGlobal = LeftKey;
            RightKeyGlobal = RightKey;
            return "已将上下左右分别设置为按键:" + UpKey + " " + DownKey + 
                " " + LeftKey + " " + RightKey; 
        }

        // 私有类
        // 按键event
        private bool isUpKeyDown = false;
        private bool isDownKeyDown = false;
        private bool isLeftKeyDown = false;
        private bool isRightKeyDown = false;
        private void Hook_KeyDown(object sender, KeyEventArgs e)
        {
            int x = picturebox.Location.X;
            int y = picturebox.Location.Y;
            /*
            //判断按下的键（Alt + A）
            if (e.KeyValue == (int)Keys.A && (int)Control.ModifierKeys == (int)Keys.Alt)
            {

            }*/
            
            if (e.KeyValue == (int)UpKeyGlobal)
            {
                isUpKeyDown = true;
                // Console.WriteLine("按下了" + UpKeyGlobal);
            }
            else if (e.KeyCode == DownKeyGlobal)
            {
                isDownKeyDown = true;
                // Console.WriteLine("按下了" + DownKeyGlobal);
            }
            else if (e.KeyCode == LeftKeyGlobal)
            {
                // Console.WriteLine(e.KeyCode);
                isLeftKeyDown = true;
                // Console.WriteLine("按下了" + LeftKeyGlobal);
            }
            else if (e.KeyCode == RightKeyGlobal)
            {
                isRightKeyDown = true;
                // Console.WriteLine("按下了" + RightKeyGlobal);
            }  
        }
        private void Hook_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == LeftKeyGlobal)
            {
                isLeftKeyDown = false;
            }
            else if (e.KeyCode == RightKeyGlobal)
            {
                isRightKeyDown = false;
            }
            else if (e.KeyCode == UpKeyGlobal)
            {
                isUpKeyDown = false;
            }
            else if (e.KeyCode == DownKeyGlobal)
            {
                isDownKeyDown = false;
            }
        }
        // 关闭窗口的时候结束线程
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (CanMoveOut)
            {
                // 注销移动函数
                mthread.Abort();
            }
            if (CanEnCircle)
            {
                // 注销包围函数
                Ethread.Abort();
            }
        }
        // 动画多线程方法
        private void AnimFor()
        {
            int Flenght = FilesNameGlobal.Length;
            int Count = 0;
            try
            {
                picturebox.Image = Image.FromFile(FilesNameGlobal[Count]);
            }
            catch (Exception e)
            {
                Console.WriteLine("程序出现了一个错误，猜测有可能是您把图床" +
                    "改为了AutoSize，请联系TWE引擎开发者或本游戏开发者进行解决" +
                    "\n具体问题(您并不需要看懂，请截图给开发者):" + e);
            }
            if (isFPSShow)
            {
                FPSLabelG.Text = "FPS:" + FPSGlobal.ToString();
            }
            while (true)
            {
                if (isClosed)
                {
                    break;
                }
                if (Count == FilesNameGlobal.Length)
                {
                    Count = 0;
                } 
                else
                {
                    picturebox.Image = Image.FromFile(FilesNameGlobal[Count]);
                    Count++;
                    Thread.Sleep(1000 / FPSGlobal);
                }
            }
        }
        // 移动多线程方法
        private void MoveThread()
        {
            while (true)
            {
                int x = picturebox.Location.X;
                int y = picturebox.Location.Y;
                // 只按了上
                if (isUpKeyDown && !isRightKeyDown && !isLeftKeyDown)
                {
                    picturebox.Location = new Point(
                    x, y - MoveSpeed);
                }
                // 只按了下
                else if (isDownKeyDown && !isRightKeyDown && !isLeftKeyDown)
                {
                    picturebox.Location = new Point(
                    x, y + MoveSpeed);
                }
                // 只按了左
                else if (isLeftKeyDown && !isUpKeyDown && !isDownKeyDown)
                {
                    picturebox.Location = new Point(
                    x - MoveSpeed, y);
                }
                
                // 只按了右
                else if (isRightKeyDown && !isUpKeyDown && !isDownKeyDown)
                {
                    picturebox.Location = new Point(
                    x + MoveSpeed, y);
                }
                // 按了上+左
                else if (isUpKeyDown && isLeftKeyDown)
                {
                    picturebox.Location = new Point(
                    x - MoveSpeed, y - MoveSpeed);
                }
                // 按了下+左
                else if (isDownKeyDown && isLeftKeyDown)
                {
                    picturebox.Location = new Point(
                    x - MoveSpeed, y + MoveSpeed);
                }
                // 按了上+右
                else if (isUpKeyDown && isRightKeyDown)
                {
                    picturebox.Location = new Point(
                    x + MoveSpeed, y - MoveSpeed);
                }
                // 按了下+右
                else if (isDownKeyDown && isRightKeyDown)
                {
                    picturebox.Location = new Point(
                    x + MoveSpeed, y + MoveSpeed);
                }
                Thread.Sleep(30);
            }
        }
        // 包围多线程方法
        private void EnCircleThread()
        {
            while (true)
            {
                form.KeyDown -= new KeyEventHandler(Hook_KeyDown);
                form.KeyUp -= new KeyEventHandler(Hook_KeyUp);
                if (BeEncirclement.Location.X <= Encirclement.Location.X + NandW)
                {
                    isLeftKeyDown = false;
                    BeEncirclement.Location = new Point(Encirclement.Location.X + NandW
                        , BeEncirclement.Location.Y);
                }
                if (BeEncirclement.Location.Y <= Encirclement.Location.Y + NandW)
                {
                    isUpKeyDown = false;
                    BeEncirclement.Location = new Point(BeEncirclement.Location.X
                        , Encirclement.Location.Y + NandW);
                }
                if (BeEncirclement.Location.X >= Encirclement.Location.X 
                    + Encirclement.Width - SandE)
                {
                    isRightKeyDown = false;
                    BeEncirclement.Location = new Point(Encirclement.Location.X
                       + Encirclement.Width - SandE , BeEncirclement.Location.Y);
                }
                if (BeEncirclement.Location.Y >= Encirclement.Location.Y
                    + Encirclement.Height - SandE)
                {
                    isDownKeyDown = false;
                    BeEncirclement.Location = new Point(BeEncirclement.Location.X
                        ,Encirclement.Location.Y + Encirclement.Height - SandE);
                }
                form.KeyDown += new KeyEventHandler(Hook_KeyDown);
                form.KeyUp += new KeyEventHandler(Hook_KeyUp);
                Thread.Sleep(5);
            }
        }
    }

    public class TweSound
    {
        private ISoundEngine IrrSoundEngine = new ISoundEngine();
        private string FileName = "";
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="MusicFileName">音乐文件位置</param>
        public void Load(string MusicFileName)
        {
            FileName = MusicFileName;
        }
        /// <summary>
        /// 播放音乐
        /// </summary>
        public void SoundPlay()
        {
            IrrSoundEngine.Play2D(FileName);
        }
        /// <summary>
        /// 停止播放音乐
        /// </summary>
        public void SoundStop()
        {
            IrrSoundEngine.StopAllSounds();
        }
        /// <summary>
        /// 暂停全部音乐
        /// </summary>
        public void SoundPaused()
        {
            IrrSoundEngine.SetAllSoundsPaused(true);
        }
        /// <summary>
        /// 恢复全部音乐
        /// </summary>
        public void SoundDispaused()
        {
            IrrSoundEngine.SetAllSoundsPaused(false);
        }
    }

    public class AnimationLib
    {
        // 需移动的项
        private Control NeedMove;

        // 移动的速度
        private int MoveSpeed;

        // 移动到的点
        private Point MoveToLocation;

        // 开始移动线程
        private Thread StartMoveThread;

        // 是否完成移动
        public bool isMoveFinish = true;

        /// <summary>
        /// 加载函数
        /// </summary>
        /// <param name="MoveObject">需要移动的物体</param>
        /// <param name="Power">移动速度</param>
        /// <param name="MTlocation">要移动到的位置</param>
        public void Load(Control MoveObject, int Power, Point MTlocation, Form fo)
        {
            NeedMove = MoveObject;
            MoveSpeed = Power;
            MoveToLocation = MTlocation;
            fo.FormClosing += new FormClosingEventHandler(Form_Closing);
            StartMoveThread = new Thread(StartThread);
        }

        /// <summary>
        /// 开始动画函数（请先load里配置好在用）
        /// </summary>
        public void StartMove()
        {
            if (StartMoveThread.IsAlive)
            {
                StartMoveThread.Join();
            }
            else
            {
                StartMoveThread.Start();
            }
        }

        /// <summary>
        /// 结束动画函数（先用开始才可以用这个，不然报错）
        /// </summary>
        public void Stop()
        {
            isMoveFinish = true;
            StartMoveThread.Abort();
        }

        // 开始线程
        private void StartThread()
        {
            while (!isMoveFinish)
            {
                Console.WriteLine(isMoveFinish);
                Thread.Sleep(100);
            }
            // 重置状态
            isMoveFinish = false;
            #region 变量设置
            // 原来的坐标
            int fx = NeedMove.Location.X;
            int fy = NeedMove.Location.Y;

            // 要移动坐标
            int nx = MoveToLocation.X;
            int ny = MoveToLocation.Y;

            // 八方
            bool isNorth = false;
            bool isSouth = false;
            bool isWest = false;
            bool isEast = false;

            bool isNaE = false;
            bool isNaW = false;
            bool isSaE = false;
            bool isSaW = false;
            #endregion

            #region 判断方位
            // 北方
            if (fx == nx && fy > ny)
            {
                isNorth = true;
            }
            // 南方
            else if (fx == nx && fy < ny)
            {
                isSouth = true;
            }
            // 西方
            else if (fx > nx && fy == ny)
            {
                isEast = true;
            }
            // 东方
            else if (fx < nx && fy == ny)
            {
                isWest = true;
            }
            // 北偏东
            else if (fx < nx && fy > ny)
            {
                isNaE = true;
            }
            // 北偏西
            else if (fx > nx && fy > ny)
            {
                isNaW = true;
            }
            // 南偏东
            else if (fx < nx && fy < ny)
            {
                isSaE = true;
            }
            // 南偏西
            else if (fx > nx && fy < ny)
            {
                isSaW = true;
            }
            #endregion

            // 本体
            while (true)
            {
                Point NowPoint;
                int fxn = NeedMove.Location.X;
                int fyn = NeedMove.Location.Y;
                #region 判断语句
                if (Math.Abs(fxn - nx) <= MoveSpeed && Math.Abs(fyn - ny) <= MoveSpeed)
                {
                    isMoveFinish = true;
                    StartMoveThread.Abort();
                }
                else if (isMoveFinish)
                {
                    isNorth = false;
                    isSouth = false;
                    isWest = false;
                    isEast = false;
                    isNaE = false;
                    isNaW = false;
                    isSaE = false;
                    isSaW = false;
                }
                if (isNorth)
                {
                    NowPoint = new Point(fxn, fyn - MoveSpeed);
                }
                else if (isSouth)
                {
                    NowPoint = new Point(fxn, fyn + MoveSpeed);
                }
                else if (isEast)
                {
                    NowPoint = new Point(fxn + MoveSpeed, fyn);
                }
                else if (isWest)
                {
                    NowPoint = new Point(fxn - MoveSpeed, fyn);
                }

                else if (isNaE)
                {
                    NowPoint = new Point(fxn + MoveSpeed, fyn - MoveSpeed);
                }
                else if (isNaW)
                {
                    NowPoint = new Point(fxn - MoveSpeed, fyn - MoveSpeed);
                }
                else if (isSaE)
                {
                    NowPoint = new Point(fxn + MoveSpeed, fyn + MoveSpeed);
                }
                else if (isSaW)
                {
                    NowPoint = new Point(fxn - MoveSpeed, fyn + MoveSpeed);
                }
                else
                {
                    NowPoint = MoveToLocation;
                }
                #endregion
                NeedMove.Location = NowPoint;
                Thread.Sleep(10);
            }
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            StartMoveThread.Abort();
        }
    }
}
