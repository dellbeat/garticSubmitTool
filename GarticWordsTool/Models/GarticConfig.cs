using GarticWordsTool.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GarticWordsTool.Models
{
    /// <summary>
    /// 首页配置
    /// </summary>
    public class GarticConfig
    {
        public Props props { get; set; }
    }

    public class Props
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public Language[] languages { get; set; }
        public Subject[] subjects { get; set; }
        public User user { get; set; }
    }

    public class User
    {
        [JsonConverter(typeof(UserIdConverter))]
        public int id { get; set; }
        public string code { get; set; }
        public string nome { get; set; }
        public int language { get; set; }
        public string versao { get; set; }
        public bool logado { get; set; }
        [JsonConverter(typeof(AvatarConverter))]
        public string? avatar { get; set; }
    }

    public class Language
    {
        public int id { get; set; }
        public string name { get; set; }
        public string iso { get; set; }
        public int active { get; set; }
        public int?[] subjects { get; set; }
    }

    public class Subject
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
