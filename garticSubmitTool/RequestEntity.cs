using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace garticSubmitTool
{
    public class RequestEntity
    {
        /// <summary>
        /// 语言代号，默认16
        /// </summary>
        public int language { get; set; }
        /// <summary>
        /// 种类代号，默认30，代表其他
        /// </summary>
        public int subject { get; set; }
        /// <summary>
        /// 添加的内容，对象为字符串列表，列表内元素分别为词汇和难度代号（默认0，即简单），如["1","0"]
        /// </summary>
        public string[][] added { get; set; }
        /// <summary>
        /// 删除的内容，格式可能也同上
        /// </summary>
        public string[] removed { get; set; }
    }

    /// <summary>
    /// 单词实体
    /// </summary>
    public class WordEntity
    {
        public string word { get; set; }

        public int code { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            WordEntity entiy = obj as WordEntity;

            return word == entiy.word;
        }
    }



}
