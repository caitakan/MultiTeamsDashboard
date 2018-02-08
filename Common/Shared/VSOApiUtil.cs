using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Microsoft.BusinessAI.Dashboard.Common.Shared
{
    public class VSOApiUtil
    {

        public static string GetResponse(string url, string token)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", token))));

                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        return responseBody;
                    }
                }
            }
            catch (Exception ex)
            {
                AppInsightLogger.Instance.Error(ex);
            }
            return null;
        }

        public static string PatchResponse(string url, string token, string content)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        System.Text.ASCIIEncoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", "", token))));


                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, url)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json-patch+json")
                };

                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    return responseBody;
                }
            }
        }
    }
}
