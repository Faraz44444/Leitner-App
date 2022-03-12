using Newtonsoft.Json;
using System;
using System.Net.Http;
using TagPortal.Core.ExternalApi.Bring.Dto.Tracking;

namespace TagPortal.Core.ExternalApi.Bring
{
    internal class BringApi
    {
        private const string BaseUri = "https://tracking.bring.com/api/v2/tracking.json";
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        private readonly HttpClient HttpClient;
        public BringApi()
        {
            if (string.IsNullOrWhiteSpace(BaseUri))
                throw new InvalidOperationException("BaseUrl is not set");
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseUri),
            };
        }

        public BringTrackingDto GetByTrackingNo(string trackingNo = "")
        {
            var result = HttpGet(new Uri(string.Format("?q={0}", trackingNo), UriKind.Relative));
            var response = JsonConvert.DeserializeObject<BringTrackingDto>(result, JsonSettings);

            if (response == null)
                throw new FeedbackException("The tracking number is either expired or not valid");

            if (response.consignmentSet.Count == 1)
            {
                var error = response.consignmentSet[0].error;

                if (error != null)
                {
                    //if(error.code == 404) throw new FeedbackException("Could not find the tracking");
                    throw new FeedbackException("The tracking number is either expired or not valid");
                }
            }

            return response;
        }

        private string HttpGet(Uri uri)
        {
            var response = HttpClient.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase ?? "Error occured when fetching data from Bring.");
            }
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
