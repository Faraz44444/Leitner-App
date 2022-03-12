using Newtonsoft.Json;
using System;
using System.Net.Http;
using TagPortal.Core.ExternalApi.Brreg.Dto;

namespace TagPortal.Core.ExternalApi.Brreg
{
    internal class BrregApi
    {
        private const string BaseUri = "https://data.brreg.no/enhetsregisteret/api/";
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        private readonly HttpClient HttpClient;
        public BrregApi()
        {
            if (string.IsNullOrWhiteSpace(BaseUri))
                throw new InvalidOperationException("BaseUrl is not set");
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseUri),
            };
        }

        public BrregPagedResponseDto GetPagedList(string name = "", string organizationNo = "", int page = 0)
        {
            var result = HttpGet(new Uri(string.Format("enheter?size=50&page={0}{1}{2}",
                                            page,
                                            string.IsNullOrWhiteSpace(name) ? "" : $"&navn={name}",
                                            string.IsNullOrWhiteSpace(organizationNo) || organizationNo.Length != 9 ? "" : $"&organisasjonsnummer={organizationNo}"), UriKind.Relative));
            return JsonConvert.DeserializeObject<BrregPagedResponseDto>(result, JsonSettings);
        }

        public BrregEnhetDto GetByOrganizationNo(string organizationNo = "")
        {
            var result = HttpGet(new Uri(string.Format("enheter/{0}", organizationNo), UriKind.Relative));
            return JsonConvert.DeserializeObject<BrregEnhetDto>(result, JsonSettings);
        }

        private string HttpGet(Uri uri)
        {
            var response = HttpClient.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase ?? "Error occured when fetching data from brreg.");
            }
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
