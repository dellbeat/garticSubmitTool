using GarticWordsTool.Converter;
using System.Text.Json.Serialization;

namespace GarticWordsTool.Models
{
    public class CookieJson
    {
        public string name { get; set; }
        [JsonConverter(typeof(CookieValueConverter))]
        public string value { get; set; }
    }
}
