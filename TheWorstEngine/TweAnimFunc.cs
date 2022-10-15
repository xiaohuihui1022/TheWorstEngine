using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

// 音频处理
using IrrKlang;
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
        // 初始化函数 懒得用get set
        public void Load(Form fo, PictureBox picture)
        {
            form = fo;
            picturebox = picture;
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
                if (BeEncirclement.Location.X <= Encirclement.Location.X + NandW)
                {
                    form.KeyDown -= new KeyEventHandler(Hook_KeyDown);
                    form.KeyUp -= new KeyEventHandler(Hook_KeyUp);
                    isLeftKeyDown = false;
                    BeEncirclement.Location = new Point(Encirclement.Location.X + NandW
                        , BeEncirclement.Location.Y);
                    form.KeyDown += new KeyEventHandler(Hook_KeyDown);
                    form.KeyUp += new KeyEventHandler(Hook_KeyUp);
                }
                if (BeEncirclement.Location.Y <= Encirclement.Location.Y + NandW)
                {
                    form.KeyDown -= new KeyEventHandler(Hook_KeyDown);
                    form.KeyUp -= new KeyEventHandler(Hook_KeyUp);
                    isUpKeyDown = false;
                    BeEncirclement.Location = new Point(BeEncirclement.Location.X
                        , Encirclement.Location.Y + NandW);
                    form.KeyDown += new KeyEventHandler(Hook_KeyDown);
                    form.KeyUp += new KeyEventHandler(Hook_KeyUp);
                }
                if (BeEncirclement.Location.X >= Encirclement.Location.X 
                    + Encirclement.Width - SandE)
                {
                    form.KeyDown -= new KeyEventHandler(Hook_KeyDown);
                    form.KeyUp -= new KeyEventHandler(Hook_KeyUp);
                    isRightKeyDown = false;
                    BeEncirclement.Location = new Point(Encirclement.Location.X
                       + Encirclement.Width - SandE , BeEncirclement.Location.Y);
                    form.KeyDown += new KeyEventHandler(Hook_KeyDown);
                    form.KeyUp += new KeyEventHandler(Hook_KeyUp);
                }
                if (BeEncirclement.Location.Y >= Encirclement.Location.Y
                    + Encirclement.Height - SandE)
                {
                    form.KeyDown -= new KeyEventHandler(Hook_KeyDown);
                    form.KeyUp -= new KeyEventHandler(Hook_KeyUp);
                    isDownKeyDown = false;
                    BeEncirclement.Location = new Point(BeEncirclement.Location.X
                        ,Encirclement.Location.Y + Encirclement.Height - SandE);
                    form.KeyDown += new KeyEventHandler(Hook_KeyDown);
                    form.KeyUp += new KeyEventHandler(Hook_KeyUp);
                }
                Thread.Sleep(1);
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
        /*
        private SoundPlayer player = new SoundPlayer();
        private bool isLoaded = false;
        // private Form form;
        // private IWavePlayer _device;
        // private AudioFileReader _reader;
        public void Load(string FileName)
        {
            // form = fo;
            player.SoundLocation = FileName;
            isLoaded = true;
        }
        public void SoundPlay()
        {
            if (!isLoaded)
            {
                throw new Exception("请先load");
            }
            player.Play();
        }
        public void SoundStop()
        {
            if (!isLoaded)
            {
                throw new Exception("请先load");
            }
            player.Stop();
        }*/
        /*
        public void WindowSound()
        {
            try
            {
                // 加载关闭事件
                form.FormClosed += new FormClosedEventHandler(Form_Closed);
                var fileName = SoundFileName;
                if (!File.Exists(fileName))
                    throw new FileNotFoundException("所选文件不存在");
                _device = new WaveOutEvent(); // Create device
                _reader = new AudioFileReader(fileName); // Create reader
                _device.Init(_reader);
                _device.PlaybackStopped += Device_OnPlaybackStopped;
            }
            catch
            {
                DisposeAll();
                throw;
            }
        }
        private void Form_Closed(object sender, EventArgs e)
        {
            DisposeAll();
        }
        private void DisposeDevice()
        {
            if (_device != null)
            {
                _device.PlaybackStopped -= Device_OnPlaybackStopped;
                _device.Dispose();
            }
        }
        private void Device_OnPlaybackStopped(object obj, StoppedEventArgs arg)
        {
            StopSound();
        }
        private void DisposeAll()
        {
            _reader?.Dispose();
            DisposeDevice();
        }
        /// <summary>
        /// 停止播放音乐
        /// </summary>
        public void StopSound()
        {
            _device?.Stop();
            if (_reader != null) _reader.Position = 0;
        }
        /// <summary>
        /// 播放音乐
        /// </summary>
        public void PlaySound()
        {
            WindowSound();
            _device?.Play();
        }
        /// <summary>
        /// 暂停音乐(可以用PlaySound继续播放)
        /// </summary>
        public void PauseSound()
        {
            _device?.Pause();
        }*/
    }

    // 废稿
    public class TweDraw
    {
        private Form DrawForm;
        private Graphics DrawEngine;
        private bool isLoaded = false;
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="f">要作画的窗口</param>
        public void Load(Form f)
        {
            DrawForm = f;
            DrawEngine = f.CreateGraphics();
            isLoaded = true;
        }
        /// <summary>
        /// 作画单个线
        /// </summary>
        /// <param name="pen">笔样式</param>
        /// <param name="point1">第一点</param>
        /// <param name="point2">第二点</param>
        public void DrawLine(Pen pen, Point point1, Point point2)
        {
            if (!isLoaded)
            {
                throw new Exception("TweDraw:请先load");
            }
            DrawEngine.DrawLine(pen, point1, point2);
        }
        /// <summary>
        /// 一堆线(按顺序连接)
        /// </summary>
        /// <param name="pen">笔样式</param>
        /// <param name="points">点数组 </param>
        public void DrawLines(Pen pen, Point[] points)
        {
            if (!isLoaded)
            {
                throw new Exception("TweDraw:请先load");
            }
            DrawEngine.DrawLines(pen, points);
        }
    }
}
