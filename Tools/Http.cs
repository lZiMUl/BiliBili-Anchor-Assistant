using System.Net.Http;

namespace BiliBili_Anchor_Assistant.Tools
{
    public class Http
    {
        public static async Task<String> Get(string url)
        {
            HttpClient httpClient = new HttpClient();
            string result = String.Empty;
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                httpResponseMessage.EnsureSuccessStatusCode();
                result = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Not Get Message");
            }
            return result;
        }
        public static async Task<string> Post(string url, string postDataStr)
        {
            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new StringContent(postDataStr);
            string result = String.Empty;
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(url, httpContent);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                httpResponseMessage.EnsureSuccessStatusCode();
                result = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            return result;
        }
    }
}
