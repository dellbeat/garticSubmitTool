using GarticWordsTool.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarticWordsTool.Utility
{
    public class CsvReader
    {
        public static (List<AdditionalWord>, List<RemovedWord>, bool, string) ReadCsv(string path)
        {
            List<AdditionalWord> additionalWords = new List<AdditionalWord>();
            List<RemovedWord> removedWords = new List<RemovedWord>();
            bool isSuccess = false;
            string msg = "";
            if (!File.Exists(path))
            {
                msg = "未找到文件，请检查路径后再试。";
            }
            else if (Path.GetExtension(path).ToLower() != ".csv")
            {
                msg = "扩展名不为csv，请确认创建的文件为CSV文件。";
            }
            else//目前没有能完全判断UTF8编码格式的方法，暂不做编码检测
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    StreamReader sr = new StreamReader(fs,Encoding.UTF8);
                    string? line;
                    while (!string.IsNullOrEmpty((line = sr.ReadLine())))
                    {
                        string[] array = line.Split(',');
                        if (array.Length != 3)
                        {
                            continue;
                        }

                        if (array[2] != "")
                        {
                            RemovedWord word = new RemovedWord(array[2]);
                            removedWords.Add(word);
                        }
                        else if (array[0] != "" && array[1] != "")
                        {
                            AdditionalWord word = new AdditionalWord(array[1], Convert.ToInt32(array[0]));
                            additionalWords.Add(word);
                        }
                    }
                    isSuccess = true;
                }
            }

            return (additionalWords, removedWords, isSuccess, msg);
        }
    }
}
