using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace TheWorstEngine
{
    public class Twe
    {
        /// <summary>
        /// 添加窗口
        /// </summary>
        public Form form;
        /// <summary>
        /// 添加图片box
        /// </summary>
        public PictureBox picturebox;
        /// <summary>
        /// 检测程序是否被关闭
        /// </summary>
        public bool isClosed = false;
        private Label FPSLabelG = new Label();
        private bool isFPSShow = false;
        
        private string[] FilesNameGolobal = { };
        private int FPSGolobal = 0;
        /// <summary>
        /// 设置窗口分辨率
        /// </summary>
        /// <param name="size">new System.Drawing.Size(1280, 720)</param>
        public string SetResolution(Size size)
        {
            form.Size = size;
            return "已将分辨率设为:" + size;
        }
        /// <summary>
        /// 设置图片用的
        /// </summary>
        /// <param name="FileName">图片路径，可以为任何图片格式(包括gif)</param>
        /// <returns></returns>
        public string NewImage(string FileName)
        {
            picturebox.Image = Image.FromFile(FileName);
            return "已将图片设置为:" + FileName;
        }

        
        /// <summary>
        /// 帧动画
        /// </summary>
        /// <param name="FilesName">文件集,将所有需要参与演算的图片路径存到这个数组里</param>
        /// <param name="FPS">帧数</param>
        /// <returns></returns>
        public string NewAnim(string[] FilesName, int FPS)
        {
            FilesNameGolobal = FilesName;
            FPSGolobal = FPS;
            Thread Anim = new Thread(AnimFor);
            Anim.Start();
            return "";
        }
        public string NewAnim(string[] FilesName, int FPS, Label FPSLable)
        {
            FilesNameGolobal = FilesName;
            FPSGolobal = FPS;
            isFPSShow = true;
            FPSLabelG = FPSLable;
            Thread Anim = new Thread(AnimFor);
            Anim.Start();
            return "";
        }
        private void AnimFor()
        {
            int Flenght = FilesNameGolobal.Length;
            int Count = 0;
            try
            {
                picturebox.Image = Image.FromFile(FilesNameGolobal[Count]);
            }
            catch (Exception e)
            {
                Console.WriteLine("程序出现了一个错误，猜测有可能是您把图床" +
                    "改为了AutoSize，请联系TWE引擎开发者或本游戏开发者进行解决" +
                    "\n具体问题(您并不需要看懂，请截图给开发者):" + e);
            }
            while (true)
            {
                if (isClosed)
                {
                    break;
                }
                if (Count == FilesNameGolobal.Length)
                {
                    Count = 0;
                } 
                else
                {
                    if (isFPSShow)
                    {
                        FPSLabelG.Text = "FPS:" + FPSGolobal.ToString();
                    }
                    picturebox.Image = Image.FromFile(FilesNameGolobal[Count]);
                    Count++;
                    Thread.Sleep(1000 / FPSGolobal);
                }
            }
        }
    }
}
