using GarticWordsTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GarticWordsTool.Net
{
    public class Gartic
    {
        private static HttpClient client;

        private string Cookie { get; set; }

        public Gartic(string cookie)
        {
            Cookie = cookie;
            InitClient();
        }

        /// <summary>
        /// 初始化HttpClient
        /// </summary>
        private void InitClient()
        {
            if (client == null)
            {
                HttpClientHandler zipHandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                };
                client = new HttpClient();
            }
        }

        /// <summary>
        /// 检查是否登录
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsLogin()
        {
            bool loginStatu = (await GetMainPage(true)).Contains("href=\"/logout\"");

            return loginStatu;
        }

        /// <summary>
        /// 获取首页内容
        /// </summary>
        /// <param name="usingCookie">是否使用<see cref="Cookie"/>，<see langword="true"/>一般为确认登录状态使用</param>
        /// <returns></returns>
        private Task<string> GetMainPage(bool usingCookie = true)
        {
            HttpRequestMessage loginMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://gartic.io/"),

            };
            loginMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36");
            //不需要额外添加解压头，会对httpclient已经设置的AutomaticDecompression属性产生影响导致无法解压
            loginMessage.Headers.Add("Connection", "keep-alive");
            loginMessage.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            if (usingCookie)
            {
                loginMessage.Headers.Add("Cookie", Cookie);
            }

            HttpResponseMessage responseMessage = client.Send(loginMessage);

            return responseMessage.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 获取语言
        /// </summary>
        /// <returns></returns>
        public async Task<LangauageEntity[]> GetLanauageConfig()
        {
            string content = await GetMainPage(false);

            Regex jsonReg = new Regex(@"(?<=""languages"":).+?\](?=,""subjects"")", RegexOptions.Singleline);

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            LangauageEntity[]? entities = JsonSerializer.Deserialize<LangauageEntity[]>(jsonReg.Match(content).Value, serializeOptions);

            return entities;
        }

        /// <summary>
        /// 获取类别的自定义词汇
        /// </summary>
        /// <param name="subject">类别代号</param>
        /// <param name="language">语种</param>
        /// <returns></returns>
        public async Task<List<WordEntity>> GetSubjectCustomWordsList(int subject, int language)
        {
            HttpRequestMessage customWordMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://gartic.io/req/subject?subject={subject}&language={language}"),

            };
            customWordMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36");
            customWordMessage.Headers.Add("Connection", "keep-alive");
            customWordMessage.Headers.Add("Accept", "application/json, text/plain, */*");
            customWordMessage.Headers.Add("Cookie", Cookie);
            customWordMessage.Headers.Add("Referer", "https://gartic.io/create");

            HttpResponseMessage responseMessage = client.Send(customWordMessage);
            string content = await responseMessage.Content.ReadAsStringAsync();

            Regex wordReg = new Regex("(?<=\\[\")[^\"]+");
            Regex codeReg = new Regex("(?<=,)\\d(?=])");

            MatchCollection wordCol = wordReg.Matches(content);
            MatchCollection codeCol = codeReg.Matches(content);

            List<WordEntity> wordEntities = new List<WordEntity>();

            for (int i = 0; i < wordCol.Count; i++)
            {
                WordEntity entity = new WordEntity(wordCol[i].Value, Convert.ToInt32(codeCol[i].Value));
                wordEntities.Add(entity);
            }

            return wordEntities;
        }
    }
}
