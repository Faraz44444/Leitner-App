using Newtonsoft.Json;
using System;
using System.Net.Http;
using TagPortal.Core.ExternalApi.DnbCurrency.Dto;

namespace TagPortal.Core.ExternalApi.DnbCurrency
{
    internal class DnbCurrencyApi
    {
        private const string BaseUri = "https://developer-api.dnb.no/currencies/";
        //private const string BaseUri = "https://developer-api-testmode.dnb.no/currencies/"; // If you are going to use test API, make sure to use test API-key

        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        private readonly HttpClient HttpClient;
        public DnbCurrencyApi(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("ApiKey is not set");
            if (string.IsNullOrWhiteSpace(BaseUri))
                throw new InvalidOperationException("BaseUrl is not set");
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseUri)
            };
            HttpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
            HttpClient.DefaultRequestHeaders.Add("accept", "*/*");
        }

        public DnbCurrencyRateResponseDto GetExchangeRate(string baseCurrency, string quoteCurrency)
        {
            if (string.IsNullOrWhiteSpace(baseCurrency)) throw new ArgumentException("baseCurrency is not set");
            if (string.IsNullOrWhiteSpace(quoteCurrency)) throw new ArgumentException("quoteCurrency is not set");

            var result = HttpGet(new Uri(string.Format("v1/{0}/convert/{1}",
                                            baseCurrency,
                                            quoteCurrency), UriKind.Relative));
            return JsonConvert.DeserializeObject<DnbCurrencyRateResponseDto>(result, JsonSettings);
        }

        private string HttpGet(Uri uri)
        {
            var response = HttpClient.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase ?? "Error occured when fetching data from DNB.");
            }
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
