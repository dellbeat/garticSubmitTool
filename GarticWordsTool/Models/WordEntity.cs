using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarticWordsTool.Models
{
    public class WordEntity
    {
        public string Word { get; private set; }

        public int Code { get; private set; }

        public WordEntity(string word,int code)
        {
            Word = word;
            Code = code;
        }

        public override bool Equals(object obj)
        {
            WordEntity? entiy = obj as WordEntity;

            return Word == entiy?.Word;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
