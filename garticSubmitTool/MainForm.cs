using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace garticSubmitTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Text +=" 最后编译时间："+ System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location);
            CheckForIllegalCrossThreadCalls = false;
        }

        Gartic gartic = null;

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            string CookieStr = Gartic.GetCookieStrFromJson(JsonText.Text);
            Thread Th = new Thread(new ParameterizedThreadStart(ApplyJson));
            Th.IsBackground = true;
            ApplyBtn.Enabled = false;
            Th.Start(CookieStr);
        }

        private void ApplyJson(object CookieStr)
        {
            gartic = new Gartic(CookieStr.ToString());
            bool statu = gartic.IsLogin();
            MessageBox.Show("Cookie已应用，当前登录状态为" + (statu ? "已登录" : "未登录"));
            ApplyBtn.Enabled = true;
        }

        private void GetWordListBtn_Click(object sender, EventArgs e)
        {
            Thread Th = new Thread(new ThreadStart(GetCurrentWords));
            Th.IsBackground = true;
            GetWordListBtn.Enabled = false;
            Th.Start();
        }

        private void GetCurrentWords()
        {
            List<WordEntity> words = gartic.GetCurrentList();
            WordLV.BeginUpdate();
            WordLV.Items.Clear();
            for (int i = 0; i < words.Count; i++)
            {
                ListViewItem item = new ListViewItem(words[i].word);
                item.SubItems.Add(words[i].code.ToString());
                WordLV.Items.Add(item);
            }
            WordLV.EndUpdate();
            GetWordListBtn.Enabled = true;
        }

        private void ClearListBtn_Click(object sender, EventArgs e)
        {
            Thread Th = new Thread(new ThreadStart(ClearWords));
            Th.IsBackground = true;
            ClearListBtn.Enabled = false;
            Th.Start();
        }

        private void ClearWords()
        {
            List<WordEntity> words = gartic.GetCurrentList();
            bool statu = gartic.UpdateWords(null, words);
            if (statu)
            {
                WordLV.BeginUpdate();
                WordLV.Items.Clear();
                WordLV.EndUpdate();
            }
            MessageBox.Show("清空词库" + (statu ? "成功" : "失败"));
            ClearListBtn.Enabled = true;
        }

        private void AddListBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "文本文件|*.txt|逗号分隔符文件|*.csv";
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Thread Th = new Thread(new ParameterizedThreadStart(UpdateWordsList));
            Th.IsBackground = true;
            AddListBtn.Enabled = false;
            Th.Start(dialog.FileName);
        }

        private void UpdateWordsList(object path)
        {
            string fullText = "";

            try
            {
                FileStream fs = new FileStream(path.ToString(), FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                fullText = sr.ReadToEnd();
                sr.Close();
                fs.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("遇到错误...请稍后再试！\r\n错误信息:\r\n" + ex.Message);
                AddListBtn.Enabled = true;
                return;
            }

            if (fullText.Substring(fullText.Length - 2, 2) == "\r\n")
            {
                fullText = fullText.Remove(fullText.Length - 2, 2);
            }
            
            string[] array = fullText.Replace("\r\n", "\t").Split('\t');//EXCEL生成的CSV文件会多一个换行符号

            List<WordEntity> words = gartic.GetCurrentList();
            int Count = words.Count;

            if (array.Length + Count < 50)
            {
                MessageBox.Show("请保证导入词库量和当前已存在词库单词总和大于等于50");
                AddListBtn.Enabled = true;
                return;
            }

            Regex NonCommaReg = new Regex(@"[^,\r\n]+");
            Regex CommaReg = new Regex(@"[^\r\n]+,[^\r\n]+");

            MatchCollection NonCommaCol = NonCommaReg.Matches(fullText);
            MatchCollection CommaCol = CommaReg.Matches(fullText);


            int ImportModeCode = 0;
            if (NonCommaCol.Count != array.Length && CommaCol.Count != array.Length)
            {
                MessageBox.Show("格式识别出现问题，请检查后再试！");
                AddListBtn.Enabled = true;
                return;
            }

            if (NonCommaCol.Count == array.Length)
            {
                ImportModeCode = 1;
            }
            else
            {
                ImportModeCode = 2;
            }

            List<WordEntity> entities = new List<WordEntity>();
            if(ImportModeCode == 1)
            {
                foreach (string str in array)
                {
                    WordEntity entity = new WordEntity() { word = str, code = 0 };
                    if (words.Contains(entity))
                    {
                        continue;
                    }
                    entities.Add(entity);
                }
            }
            else
            {
                foreach(string str in array)
                {
                    string[] strArr = str.Split(',');
                    if(!int.TryParse(strArr[1],out int code))
                    {
                        continue;
                    }
                    WordEntity entity = new WordEntity() { word = strArr[0], code = code };
                    entities.Add(entity);
                }
            }

            bool statu = gartic.UpdateWords(entities, null);

            if (statu)
            {
                words = gartic.GetCurrentList();
                WordLV.BeginUpdate();
                WordLV.Items.Clear();
                for (int i = 0; i < words.Count; i++)
                {
                    ListViewItem item = new ListViewItem(words[i].word);
                    item.SubItems.Add(words[i].code.ToString());
                    WordLV.Items.Add(item);
                }
                WordLV.EndUpdate();//更新词汇列表
            }

            MessageBox.Show("追加词库" + (statu ? "成功" : "失败"));

            AddListBtn.Enabled = true;
        }
    }
}
