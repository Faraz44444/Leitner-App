using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace TagPortal.Core.ExternalApi
{


    abstract class BaseApi
    {
        private readonly string BaseUrl;


        private readonly HttpClient HttpClient;


        public BaseApi(string baseUrl, string authToken = null, string basicAuthUsername = null, string basicAuthPassword = null)
        {
            BaseUrl = baseUrl;


            if (string.IsNullOrWhiteSpace(BaseUrl))
                throw new InvalidOperationException("BaseUrl is not set");


            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl),
            };


            bool hasAuth = false;
            if (!string.IsNullOrEmpty(authToken))
            {
                hasAuth = true;
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }
            if (!string.IsNullOrEmpty(basicAuthUsername) || !string.IsNullOrEmpty(basicAuthPassword))
            {
                if (hasAuth) throw new ArgumentException("Token based authentication has already been added");
                if (string.IsNullOrEmpty(basicAuthUsername)) throw new ArgumentException("basicAuthUsername is not set", "basicAuthUsername");
                if (string.IsNullOrEmpty(basicAuthPassword)) throw new ArgumentException("basicAuthPassword is not set", "basicAuthPassword");


                var byteArray = Encoding.ASCII.GetBytes($"{basicAuthUsername}:{basicAuthPassword}");
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            }


            HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("wiseBaseAPI", "1.0"));
        }

        public void SetToken(string authToken)
        {
            if (string.IsNullOrEmpty(authToken)) throw new ArgumentException("AuthToken is not set", "AuthToken");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        private string GetQueryString(object queryParameters)
        {
            if (queryParameters == null) return "";


            var properties = queryParameters.GetType().GetProperties()
                .Where(p => p.GetValue(queryParameters, null) != null)
                .Select(p => p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(queryParameters, null).ToString()));


            return string.Join("&", properties.ToArray());
        }


        protected TResponse HttpGet<TResponse>(string uri, object queryParameters = null)
        {
            var _uri = new Uri(uri + (queryParameters == null ? "" : "?" + GetQueryString(queryParameters)), UriKind.Relative);
            var response = HttpClient.GetAsync(_uri).Result;
            var responseStr = response.Content.ReadAsStringAsync().Result;



            if (!response.IsSuccessStatusCode)
                throw new ApiException(response.StatusCode, responseStr);


            return JsonConvert.DeserializeObject<TResponse>(responseStr);
        }


        protected HttpResponseMessage HttpPost(string uri, object body = null)
        {
            var _uri = new Uri(uri, UriKind.Relative);
            return HttpClient.PostAsJsonAsync(_uri, body).Result;
        }


        protected TResponse HttpPost<TResponse>(string uri, object body = null)
        {
            var response = HttpPost(uri, body);
            var responseStr = response.Content.ReadAsStringAsync().Result;


            if (!response.IsSuccessStatusCode)
                throw new ApiException(response.StatusCode, responseStr);


            return JsonConvert.DeserializeObject<TResponse>(responseStr);
        }


        protected HttpResponseMessage HttpPut(string uri, object body = null)
        {
            var _uri = new Uri(uri, UriKind.Relative);
            return HttpClient.PutAsJsonAsync(_uri, body).Result;
        }


        protected TResponse HttpPut<TResponse>(string uri, object body = null)
        {
            var response = HttpPut(uri, body);
            var responseStr = response.Content.ReadAsStringAsync().Result;


            if (!response.IsSuccessStatusCode)
                throw new ApiException(response.StatusCode, responseStr);


            return JsonConvert.DeserializeObject<TResponse>(responseStr);
        }


        protected HttpResponseMessage HttpDeleteAsync(string uri)
        {
            var _uri = new Uri(uri, UriKind.Relative);
            return HttpClient.DeleteAsync(_uri).Result;
        }
    }
}















