using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarticWordsTool.Models
{
    public class WordEntity
    {
        public string Word { get; protected set; }

        public int Code { get; protected set; }

        public WordEntity(string word,int code)
        {
            Word = word;
            Code = code;
        }

        public WordEntity(WordEntity entity)
        {
            Word = entity.Word;
            Code = entity.Code;
        }

        public override bool Equals(object? obj)
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
