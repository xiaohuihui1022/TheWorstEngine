using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheWorstEngine.Developer
{
    public class ValueGet
    {
        // 全局受伤音效
        public string GlobalHurtSound { get; set; }
        // 玩家血量
        public int PlayerHealth { get; set; } = 100;
        // 显示血量Label
        public Label HealthCount { get; set; }
        // 游戏窗口
        public Form form { get; set; }
        // 设置判定线
        public PictureBox Line { get; set; }
        // 渲染用Panel
        public Panel Renderer { get; set; }
        // SansPictureBox
        public PictureBox SansPicture { get; set; }
        public PictureBox HeartPicture { get; set; }

        public int PlayerMoveSpeed { get; set; } = 5;
        public int EnemyMoveSpeed { get; set; } = 5;
    }
}
