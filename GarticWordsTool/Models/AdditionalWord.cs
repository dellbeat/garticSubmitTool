using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarticWordsTool.Models
{
    /// <summary>
    /// 需要添加的单词实体
    /// </summary>
    public class AdditionalWord : WordEntity
    {
        public AdditionalWord(string word, int code) : base(word, code)
        {

        }

        public override string ToString()
        {
            return $"[\"{Word}\",{Code}]";
        }
    }
}
