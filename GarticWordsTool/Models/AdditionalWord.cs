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
