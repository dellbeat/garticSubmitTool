using CsharpHttpHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace garticSubmitTool
{
    public class Gartic
    {
        private string Cookie { get; set; }

		public Gartic(string cookie)
        {
			Cookie = cookie;
        }

		/// <summary>
		/// 获取当前个人自定义词库列表
		/// </summary>
		/// <param name="subject"></param>
		/// <param name="lanauage"></param>
		/// <returns></returns>
        public List<WordEntity> GetCurrentLisy(int subject = 30,int lanauage = 16)
        {
			HttpHelper helper = new HttpHelper();
			HttpItem item = new HttpItem
			{
				URL = "https://gartic.io/req/subject?subject=" + subject + "&language=" + lanauage,
				Method = "GET",
				Host = "gartic.io",
				KeepAlive = true,
				Accept = "application/json, text/plain, */*",
				UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36",
				Referer = "https://gartic.io/create",
				Cookie = Cookie,
				WebProxy = System.Net.WebProxy.GetDefaultProxy(),
			};
			item.Header["sec-ch-ua"] = "\"Chromium\";v=\"88\" \"Google Chrome\";v=\"88\", \"; Not A Brand\";v=\"99\"";
			item.Header["DNT"] = "1";
			item.Header["sec-ch-ua-mobile"] = "?0";
			item.Header["Sec-Fetch-Site"] = "same-origin";
			item.Header["Sec-Fetch-Mode"] = "cors";
			item.Header["Sec-Fetch-Dest"] = "empty";
			item.Header["Accept-Encoding"] = "gzip, deflate, br";
			item.Header["Accept-Language"] = "zh-CN,zh;q=0.9,en;q=0.8,ja;q=0.7";
			item.Header["If-None-Match"] = "W/\"1b0-6pV99KjPaMFjkiQaRDDGBPUwIMk\"";

			HttpResult result = helper.GetHtml(item);

			Regex wordReg = new Regex("(?<=\\[\")[^\"]+");
			Regex codeReg = new Regex("(?<=,)\\d(?=])");

			MatchCollection wordCol = wordReg.Matches(result.Html);
			MatchCollection codeCol = codeReg.Matches(result.Html);

			List<WordEntity> wordEntities = new List<WordEntity>();

			for(int i = 0; i < wordCol.Count; i++)
            {
				WordEntity entity = new WordEntity();
				entity.word = wordCol[i].Value;
				entity.code = Convert.ToInt32(codeCol[i].Value);
				wordEntities.Add(entity);
            }

			return wordEntities;
		}

		/// <summary>
		/// 更新词库
		/// </summary>
		/// <returns></returns>
		public bool UpdateWords(List<WordEntity> addWords,List<WordEntity> removeWords)
        {
			string addStr = "";
			string removeStr = "";

			for(int i = 0; i < (addWords == null ? 0 : addWords.Count); i++)
            {
                if (addStr != "")
                {
					addStr += ",";
                }
				addStr += "[\"" + addWords[i].word + "\",\"" + addWords[i].code + "\"]";
            }

			for(int i = 0; i < (removeWords == null ? 0 : removeWords.Count); i++)
            {
                if (removeStr != "")
                {
					removeStr += ",";
                }
				removeStr += "\"" + removeWords[i].word + "\"";
            }

			string str = "{\"language\":16,\"subject\":30,\"added\":[" + addStr + "],\"removed\":[" + removeStr + "]}";

			HttpHelper helper = new HttpHelper();
			HttpItem item = new HttpItem
			{
				URL = "https://gartic.io/req/editSubject",
				Method = "POST",
				Host = "gartic.io",
				KeepAlive = true,
				Accept = "application/json, text/plain, */*",
				UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36",
				ContentType = "application/json;charset=UTF-8",
				Referer = "https://gartic.io/theme",
				Cookie = Cookie,
				WebProxy = System.Net.WebProxy.GetDefaultProxy(),
				PostDataType = CsharpHttpHelper.Enum.PostDataType.Byte,
				PostEncoding = Encoding.UTF8,
				PostdataByte = Encoding.UTF8.GetBytes(str)
			};
			item.Header["sec-ch-ua"] = "\"Chromium\";v=\"88\",\"Google Chrome\";v=\"88\", \";Not A Brand\";v=\"99\"";
			item.Header["DNT"] = "1";
			item.Header["sec-ch-ua-mobile"] = "?0";
			item.Header["Origin"] = "https://gartic.io";
			item.Header["Sec-Fetch-Site"] = "same-origin";
			item.Header["Sec-Fetch-Mode"] = "cors";
			item.Header["Sec-Fetch-Dest"] = "empty";
			item.Header["Accept-Encoding"] = "gzip, deflate, br";
			item.Header["Accept-Language"] = "zh-CN,zh;q=0.9,en;q=0.8,ja;q=0.7";

			HttpResult result = helper.GetHtml(item);

			return result.Html.Contains("true");
		}

		/// <summary>
		/// Cookie JSON转字符串
		/// </summary>
		/// <param name="json"></param>
		/// <returns></returns>
		public static string GetCookieStrFromJson(string json)
		{
			Regex AreaReg = new Regex("{.+?}", RegexOptions.Singleline);
			Regex NameReg = new Regex("\"name\": \"[^\"]+");
			Regex ValueReg = new Regex("\"value\": \"[^\"]+");
			MatchCollection AreaMatch = AreaReg.Matches(json);
			string cookie = "";
			foreach (Match i in AreaMatch)
			{
				if (cookie != "")
				{
					cookie += ";";
				}
				if (NameReg.IsMatch(i.Value) && ValueReg.IsMatch(i.Value))
				{
					cookie += NameReg.Match(i.Value).Value.Replace("\"name\": \"", "") + "=" + ValueReg.Match(i.Value).Value.Replace("\"value\": \"", "");
				}
			}

			return cookie;
		}
	}
}
