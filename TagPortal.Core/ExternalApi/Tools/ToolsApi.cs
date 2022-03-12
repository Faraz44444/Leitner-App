using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using TagPortal.Core.ExternalApi.Tools.Dto;

namespace TagPortal.Core.ExternalApi.Tools
{
    internal class ToolsApi
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        private readonly HttpClient HttpClient;
        public ToolsApi(string baseUri)
        {
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUri)
            };
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public PagedToolsProductDto GetArticles(string username, string password, int currentPage, int itemsPerPage, DateTime fetchArticlesUpdatedSince, string transactionId = "")
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("username is not set");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("password is not set");
            if (itemsPerPage < 1) throw new ArgumentException("itemsPerPage is not set");
            if (fetchArticlesUpdatedSince == null || fetchArticlesUpdatedSince == DateTime.MinValue) throw new ArgumentException("fetchArticlesUpdatedSince is not set");

            string url = string.Format("punchout?username={0}&password={1}&function=downloadjson&pagesize={2}&lastupdated={3}",
                username,
                password,
                itemsPerPage,
                fetchArticlesUpdatedSince.ToString("ddMMyyyyHHmmss"));
            if (currentPage > 0)
                url += string.Format("&requestedpage={0}", currentPage);
            if (!string.IsNullOrWhiteSpace(transactionId))
                url += string.Format("&transactionid={0}", transactionId);

            var result = HttpGet(new Uri(url, UriKind.Relative));
            return JsonConvert.DeserializeObject<PagedToolsProductDto>(result, JsonSettings);
        }

        private string HttpGet(Uri uri)
        {
            var response = HttpClient.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase ?? "Error occured when fetching data from Tools integration.");
            }
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
