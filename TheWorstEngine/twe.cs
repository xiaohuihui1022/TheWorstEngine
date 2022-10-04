﻿using System;
using System.Drawing;
using System.Threading;
using NAudio.Wave;
using System.Windows.Forms;
using System.IO;
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
        // FPS显示
        private Label FPSLabelG = new Label();
        // 是否显示FPS
        private bool isFPSShow = false;
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
        // 动画图片
        private string[] FilesNameGlobal = { };
        // 动画FPS
        private int FPSGlobal = 0;
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
        /// 帧动画
        /// </summary>
        /// <param name="FilesName">文件集,将所有需要参与演算的图片路径存到这个数组里</param>
        /// <param name="FPS">帧数</param>
        /// <param name="FPSLabel">绑定帧数显示label</param>
        /// <returns></returns>
        public string SetAnim(string[] FilesName, int FPS, Label FPSLabel)
        {
            isFPSShow = true;
            FPSLabelG = FPSLabel;
            SetAnim(FilesName, FPS);
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
                /*
                KeyManager k_hook = new KeyManager();
                k_hook.KeyDownEvent += new KeyEventHandler(hook_KeyDown);//钩住键按下
                k_hook.Start();//安装键盘钩子*/
                form.KeyDown += new KeyEventHandler(Hook_KeyDown);
                form.KeyUp += new KeyEventHandler(Hook_KeyUp);
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
            if (e.KeyCode == UpKeyGlobal)
            {
                isUpKeyDown = true;
                if (isLeftKeyDown)
                {
                    picturebox.Location = new Point(
                    x - MoveSpeed, y - MoveSpeed);
                }
                else if (isRightKeyDown)
                {
                    picturebox.Location = new Point(
                    x + MoveSpeed, y - MoveSpeed);
                }
                else
                {
                    picturebox.Location = new Point(
                    x, y - MoveSpeed);
                }
                // Console.WriteLine("按下了" + UpKeyGlobal);
            }
            else if (e.KeyCode == DownKeyGlobal)
            {
                isDownKeyDown = true;
                if (isLeftKeyDown)
                {
                    picturebox.Location = new Point(
                    x - MoveSpeed, y + MoveSpeed);
                }
                else if (isRightKeyDown)
                {
                    picturebox.Location = new Point(
                    x + MoveSpeed, y + MoveSpeed);
                }
                else
                {
                    picturebox.Location = new Point(
                    x, y + MoveSpeed);
                }
                // Console.WriteLine("按下了" + DownKeyGlobal);
            }
            else if (e.KeyCode == LeftKeyGlobal)
            {
                isLeftKeyDown = true;
                if (isUpKeyDown)
                {
                    picturebox.Location = new Point(
                    x - MoveSpeed, y - MoveSpeed);
                }
                else if (isDownKeyDown)
                {
                    picturebox.Location = new Point(
                    x - MoveSpeed, y + MoveSpeed);
                }
                else
                {
                    picturebox.Location = new Point(
                    x - MoveSpeed, y);
                }
                // Console.WriteLine("按下了" + LeftKeyGlobal);
            }
            else if (e.KeyCode == RightKeyGlobal)
            {
                isRightKeyDown = true;
                if (isUpKeyDown)
                {
                    picturebox.Location = new Point(
                    x + MoveSpeed, y - MoveSpeed);
                }
                else if (isDownKeyDown)
                {
                    picturebox.Location = new Point(
                    x + MoveSpeed, y + MoveSpeed);
                }
                else
                {
                    picturebox.Location = new Point(
                    x + MoveSpeed, y);
                }
                
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
        
    }
    public class TweSound
    {
        private Form form;
        private string SoundFileName;
        private IWavePlayer _device;
        private AudioFileReader _reader;
        public void Load(Form fo, string FileName)
        {
            form = fo;
            SoundFileName = FileName;
        }
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
            _device?.Play();
        }
        /// <summary>
        /// 暂停音乐(可以用PlaySound继续播放)
        /// </summary>
        public void PauseSound()
        {
            _device?.Pause();
        }
    }
}
