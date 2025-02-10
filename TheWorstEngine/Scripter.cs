using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorstEngine.Developer;
using TheWorstEngine.AnimFunction;
using TheWorstEngine.AttackCombo;
using TheWorstEngine.UIFunction;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data;
using System.Threading;

namespace TheWorstEngine.Developer
{
    public class Scripter
    {
        
        // 获取玩家状态
        public ValueGet Valueget { get; set; }
        // 定义存储函数信息
        private Hashtable TempVars = new Hashtable();
        private string[] script;
        private Thread MainThread;
        private Thread CheckGameOverThread;
        

        public void LoadScript(string Script)
        {
            // 清空TempVars
            TempVars.Clear();
            // 写入脚本
            script = File.ReadAllLines(Script);
            // 绑定Threads
            MainThread = new Thread(Main);
            MainThread.Start();

            CheckGameOverThread = new Thread(CheckGameOver);
            CheckGameOverThread.Start();
        }

        private void Main()
        {
            foreach (string line in script)
            {
                // 如果有赋值
                if (line.Contains("="))
                {
                    if (line.Contains("InitEnemy"))
                    {
                        TweAttackCombo Temp = new TweAttackCombo();
                        string[] args = MidStrEx(line, "(", ")").Replace(" ", "").Split(',');
                        Temp.Load(
                            Valueget,
                            args[0],
                            int.Parse(args[1]),
                            int.Parse(args[2]),
                            int.Parse(args[3]),
                            int.Parse(args[4]
                        ));
                        Temp.AutoAttackCheckSet(Valueget.HeartPicture, 5, 50);
                        TempVars.Add(line.Replace(" ", "").Split('=')[0], Temp);
                    }
                }

                // 如果有执行
                if (line.Contains("."))
                {
                    string[] executable = line.Replace(" ", "").Split('.');
                    Console.WriteLine(executable[0]);
                    if (TempVars.ContainsKey(executable[0]))
                    {
                        TweAttackCombo Temp = (TweAttackCombo)TempVars[executable[0]];

                        // 执行指令
                        if (executable[1].Contains("MoveTo"))
                        {
                            // 取移动到的地方，并执行
                            string[] Location = MidStrEx(executable[1], "(", ")").Split(',');
                            Temp.MoveTo(int.Parse(Location[0]), int.Parse(Location[1]));
                            Console.WriteLine("has moved to " + int.Parse(Location[0]) + ", " + int.Parse(Location[1]));
                        }
                    }

                }

                // 如果有等待
                if (line.Contains("Wait"))
                {
                    Thread.Sleep(1000 * int.Parse(MidStrEx(line, "(", ")")));
                }
            }
        }

        /// <summary>
        /// 取文本中间算法
        /// </summary>
        /// <param name="sourse">源文本</param>
        /// <param name="startstr">头参考文本</param>
        /// <param name="endstr">尾参考文本</param>
        /// <returns></returns>
        public static string MidStrEx(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {
                throw new Exception("MidStrEx Err:" + ex.Message);
            }
            return result;
        }

        private void CheckGameOver()
        {
            while (true)
            {
                if (Valueget.PlayerHealth <= 0)
                {
                    foreach (TweAttackCombo Temp in TempVars.Values)
                    {
                        // 销毁Enemy
                        Temp.EnemyDestroy();
                    }
                }
            }
        }


    }
}
