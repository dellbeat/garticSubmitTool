namespace GarticWordsTool.Models
{
    /// <summary>
    /// 需要移除的单词实体
    /// </summary>
    public class RemovedWord : WordEntity
    {
        public RemovedWord(WordEntity entity) : base(entity)
        {

        }

        public RemovedWord(string word, int code = -1) : base(word, code)
        {

        }

        public override string ToString()
        {
            return $"\"{Word}\"";
        }
    }
}
