using GarticWordsTool.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        static Gartic()
        {
            HttpClientHandler zipHandler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            };
            client = new HttpClient();
        }

        private static HttpClient client;

        private static Regex jsonReg = new Regex(@"(?<=application/json""\>).+?(?=\</script\>)", RegexOptions.Singleline);

        private string Cookie { get; set; }

        public Gartic(string cookie)
        {
            Cookie = cookie;
        }

        /// <summary>
        /// 更新为其他Cookie
        /// </summary>
        /// <param name="cookie"></param>
        public void UpdateCookie(string cookie)
        {
            Cookie = cookie;
        }

        /// <summary>
        /// 检查是否登录,并返回配置
        /// </summary>
        /// <returns></returns>
        public async Task<GarticConfig> GetLoginProfile()
        {
            return await GetConfigAsync(true);
        }

        /// <summary>
        /// 获取登录后的头像
        /// </summary>
        /// <returns></returns>
        public async Task<Bitmap> GetAvatar(string avatarUrl)
        {
            Bitmap bitmap = default;
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(avatarUrl)
            };
            message.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36");
            message.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");

            HttpResponseMessage responseMessage = await client.SendAsync(message);

            try
            {
                Stream bitmapStream = await responseMessage.Content.ReadAsStreamAsync();
                bitmap = new Bitmap(bitmapStream);
            }
            catch
            {

            }

            return bitmap;
        }

        /// <summary>
        /// 获取首页内容
        /// </summary>
        /// <param name="usingCookie">是否使用<see cref="Cookie"/></param>
        /// <returns></returns>
        private Task<string> GetMainPage(bool usingCookie)
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
        /// 获取配置内部方法
        /// </summary>
        /// <param name="useCookie">是否使用<see cref="Cookie">Cookie</see></param>
        /// <returns></returns>
        private async Task<GarticConfig?> GetConfigAsync(bool useCookie)
        {
            string content = await GetMainPage(useCookie);
            GarticConfig? config = JsonSerializer.Deserialize<GarticConfig>(jsonReg.Match(content).Value);

            return config;
        }

        /// <summary>
        /// 获取语言
        /// </summary>
        /// <returns></returns>
        public async Task<Language[]> GetLanauageConfig()
        {
            GarticConfig? config = await GetConfigAsync(false);

            return config?.props.data.languages;
        }

        /// <summary>
        /// 获取类别的自定义词汇
        /// </summary>
        /// <param name="subject">类别代号</param>
        /// <param name="language">语种</param>
        /// <returns>词汇列表</returns>
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

        /// <summary>
        /// 更新指定语种指定类别的词库
        /// </summary>
        /// <param name="language">语种代号</param>
        /// <param name="subject">类别</param>
        /// <param name="additionalWords">添加的词汇列表</param>
        /// <param name="removedWords">删除的词汇列表</param>
        /// <returns>更新状态</returns>
        public async Task<bool> UpdateWordsAsync(int language, int subject, List<AdditionalWord> additionalWords, List<RemovedWord> removedWords)
        {
            string sendStr = "{\"language\":" + language + ",\"subject\":" + subject + ",\"added\":[" + string.Join(",", additionalWords) + "],\"removed\":[" + string.Join(",", removedWords) + "]}";

            StringContent sendContent = new StringContent(sendStr, Encoding.UTF8, "application/json");

            HttpRequestMessage updateMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://gartic.io/req/editSubject"),
                Content = sendContent,
            };

            updateMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36");
            updateMessage.Headers.Add("Connection", "keep-alive");
            updateMessage.Headers.Add("Accept", "application/json, text/plain, */*");
            updateMessage.Headers.Add("Cookie", Cookie);
            updateMessage.Headers.Add("Referer", "https://gartic.io/theme");

            HttpResponseMessage responseMessage = client.Send(updateMessage);
            string content = await responseMessage.Content.ReadAsStringAsync();

            bool updateStatu = content.Contains("true");

            return updateStatu;
        }

        /// <summary>
        /// 删除类别下的自定义词库
        /// </summary>
        /// <param name="language">语种代号</param>
        /// <param name="subject">类别</param>
        /// <returns>是否删除成功</returns>
        public async Task<bool> DeleteSubject(int language, int subject)
        {
            string sendStr = "{\"language\":" + language + ",\"subject\":" + subject + "}";

            StringContent sendContent = new StringContent(sendStr, Encoding.UTF8, "application/json");

            HttpRequestMessage deleteMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://gartic.io/req/removeSubject"),
                Content = sendContent,
            };

            deleteMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36");
            deleteMessage.Headers.Add("Connection", "keep-alive");
            deleteMessage.Headers.Add("Accept", "application/json, text/plain, */*");
            deleteMessage.Headers.Add("Cookie", Cookie);
            deleteMessage.Headers.Add("Referer", "https://gartic.io/theme");

            HttpResponseMessage responseMessage = client.Send(deleteMessage);
            string content = await responseMessage.Content.ReadAsStringAsync();

            bool updateStatu = content.Contains("true");

            return updateStatu;
        }
    }
}
