using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace com.lZiMUl.BiliBili_Anchor_Assistant.Tools
{
    public class Http
    {
        public static async Task<T> Get<T>(string url)
        {
            var httpClient = new HttpClient();
            string result = string.Empty;
            var httpResponseMessage = await httpClient.GetAsync(url);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                httpResponseMessage.EnsureSuccessStatusCode();
                result = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Not Get Message");
            }
            return JsonConvert.DeserializeObject<T>(result);
        }
        public static async Task<string> Post(string url, string postDataStr)
        {
            var httpClient = new HttpClient();
            HttpContent httpContent = new StringContent(postDataStr);
            string result = string.Empty;
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var httpResponseMessage = await httpClient.PostAsync(url, httpContent);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                httpResponseMessage.EnsureSuccessStatusCode();
                result = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            return result;
        }
    }
}
