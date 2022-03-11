using GarticWordsTool.Models;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace GarticWordsTool.Utility
{
    public class CookieUtility
    {
        private static Regex KeyValueReg = new Regex("[^;=]+=[^;=]+(;[^;=]+=[^;=]+)*", RegexOptions.Singleline);

        private static Regex JsonValueReg = new Regex(@"\[\s*{[^}]+}(,\s*{[^}]+})+\s*\]", RegexOptions.Singleline);

        /// <summary>
        /// 将从EditThisCookie导出的Cookie转换为键值形式
        /// </summary>
        /// <param name="jsonCookie">传入的Json</param>
        /// <returns>转换后的Cookie字符串</returns>
        private static string JsonToKeyValue(string jsonCookie)
        {
            var entity = JsonSerializer.Deserialize<CookieJson[]>(jsonCookie);

            StringBuilder sb = new StringBuilder();
            foreach (var kv in entity)
            {
                if (sb.Length > 0)
                {
                    sb.Append(";");
                }
                sb.Append($"{kv.name}={kv.value}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 根据传入Cookie获取有效键值Cookie
        /// </summary>
        /// <param name="jsonCookie">传入的Cookie字符串或Json</param>
        /// <returns>如果符合格式要求则返回转换后的Cookie字符串，否则返回空字符串</returns>
        public static string ConvertCookie(string jsonCookie)
        {
            if (JsonValueReg.IsMatch(jsonCookie))
            {
                return JsonToKeyValue(jsonCookie);
            }
            else if (KeyValueReg.IsMatch(jsonCookie))
            {
                return jsonCookie;
            }
            else
            {
                return "";
            }
        }
    }
}
