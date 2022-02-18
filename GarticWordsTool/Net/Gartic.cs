using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
                HttpClientHandler httpClientHandler = new HttpClientHandler()
                {
                    Proxy = WebRequest.GetSystemWebProxy(),
                    UseProxy = true,
                };
                client = new HttpClient(httpClientHandler);
            }
        }

        public async Task<bool> IsLogin()
        {
            bool loginStatu = false;
            HttpRequestMessage loginMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://gartic.io/"),

            };
            loginMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36");
            loginMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            loginMessage.Headers.Add("Connection", "keep-alive");
            loginMessage.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            loginMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            loginMessage.Headers.Add("Cookie", Cookie);

            HttpResponseMessage responseMessage = client.Send(loginMessage);

            loginStatu =  (await responseMessage.Content.ReadAsStringAsync()).Contains("href=\"/logout\"");

            return loginStatu;
        }
    }
}
