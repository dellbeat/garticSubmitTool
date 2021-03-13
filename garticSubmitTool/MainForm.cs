using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace garticSubmitTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Text +=" 最后编译时间："+ System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location);
            //Gartic tic = new Gartic("__cfduid=d6575c836187bb1c35596a48835b139e91615379789; garticio=s%3A6d701c4c-24d6-4f13-a3bc-9c94903dded4.S6B1pe3KnzoP6AFT093RKN%2Bdigh0aImuZFvoYOk0ghM; _gid=GA1.2.559664373.1615552304; _gat_gtag_UA_3906902_31=1; __cf_bm=3d60fd09da53b7f52071deac73860d502ced6c6b-1615608185-1800-AfgtErXXjZiEjSdR4pvYmbh9G6nVe+SWdPg8OE3MmCrZ/IWe0CcwnJV/1lTb7AQcutu4of6HXuVj/OBoqURSNBY=; _ga_VR1WBQ9P5N=GS1.1.1615608207.33.1.1615608221.0; _ga=GA1.2.846823629.1612770039");
            //List<WordEntity> words = tic.GetCurrentLisy();
            //WordEntity entity = new WordEntity();
            //entity.word = "Hello";
            //entity.code = 0;
            //List<WordEntity> addCol = new List<WordEntity>();
            //addCol.Add(entity);

            //MessageBox.Show(tic.UpdateWords(addCol, null).ToString());
            //words = tic.GetCurrentLisy();
            //MessageBox.Show(tic.UpdateWords(null, addCol).ToString());
            //words = tic.GetCurrentLisy();
            //MessageBox.Show("OK");
        }

        Gartic gartic = null;

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            string CookieStr = Gartic.GetCookieStrFromJson(JsonText.Text);
            gartic = new Gartic(CookieStr);
            MessageBox.Show("Cookie初始化成功，请点击右侧按钮，如可获取到内容则表示Cookie有效，否则请检查");
        }

        private void GetWordListBtn_Click(object sender, EventArgs e)
        {
            List<WordEntity> words = gartic.GetCurrentLisy();
            WordLV.BeginUpdate();
            WordLV.Items.Clear();
            for(int i = 0; i < words.Count; i++)
            {
                ListViewItem item = new ListViewItem(words[i].word);
                item.SubItems.Add(words[i].code.ToString());
                WordLV.Items.Add(item);
            }
            WordLV.EndUpdate();
        }

        private void ClearListBtn_Click(object sender, EventArgs e)
        {
            List<WordEntity> words = gartic.GetCurrentLisy();
            bool statu = gartic.UpdateWords(null, words);
            if (statu)
            {
                WordLV.BeginUpdate();
                WordLV.Items.Clear();
                WordLV.EndUpdate();
            }
            MessageBox.Show("清空词库" + (statu ? "成功" : "失败"));
        }

        private void AddListBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "文本文件|*.txt";
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            FileStream fs = new FileStream(dialog.FileName,FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            string fullText = sr.ReadToEnd();
            sr.Close();
            fs.Close();

            string[] array = fullText.Replace("\r\n", "\t").Split('\t');

            List<WordEntity> words = gartic.GetCurrentLisy();
            int Count = words.Count;

            if (array.Length + Count < 50)
            {
                MessageBox.Show("请保证导入词库量和当前已存在词库单词总和大于等于50");
                return;
            }

            List<WordEntity> entities = new List<WordEntity>();
            foreach(string str in array)
            {
                entities.Add(new WordEntity() { word = str, code = 0 });
            }

            bool statu = gartic.UpdateWords(entities, null);

            if (statu)
            {
                words = gartic.GetCurrentLisy();
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
        }
    }
}
