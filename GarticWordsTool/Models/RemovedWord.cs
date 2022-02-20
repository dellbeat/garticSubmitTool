using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarticWordsTool.Models
{
    /// <summary>
    /// 需要移除的单词实体
    /// </summary>
    public class RemovedWord : WordEntity
    {
        /// <summary>
        /// 传入<see cref="WordEntity"/>实体即可，因为被删除的单词一定是存在的
        /// </summary>
        /// <param name="entity"></param>
        public RemovedWord(WordEntity entity) : base(entity)
        {

        }

        public override string ToString()
        {
            return $"\"{Word}\"";
        }
    }
}
