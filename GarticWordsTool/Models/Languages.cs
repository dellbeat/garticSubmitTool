using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarticWordsTool.Models
{
    public class Languages
    {
        //public static string[] LanguageArray = { "Português", "English", "Español", "Français", "Italiano", "Русский", "Türkçe", "中文 (臺灣)", "Polski", "Čeština", "ภาษาไทย", "Tiếng Việt", "Deutsch", "日本語"
        //    ,"中文 (简化字)","中文 (香港)","Nederlands","العربية","한국어"};

    }


    public class LangauageEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iso { get; set; }
        public int Active { get; set; }
        public int[] Subjects { get; set; }
    }

}
