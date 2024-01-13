namespace GarticLib.Models
{
    public class WordEntity
    {
        public string Word { get; set; }

        public WordLevelEnum Level { get; set; }

        public override bool Equals(object obj)
        {
            WordEntity entity = obj as WordEntity;

            return Word == entity?.Word;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
