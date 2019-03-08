using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SharpHive.Models.WebHCat
{
    public abstract class WebHCatRequester
    {
        #region Helpers
        protected async Task<T> Get<T>(string webHCatBaseUrl, string webHCatVersion, string webHCatUserName, string WebHCatAPI) where T : new()
        {
            var result = new T();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    client.BaseAddress = new Uri(webHCatBaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                    HttpResponseMessage response = await client.GetAsync(WebHCatAPI);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(jsonString);
                }
            }

            return result;
        }

        protected async Task<T> Post<T>(string webHCatBaseUrl, string webHCatVersion, string webHCatUserName, string WebHCatAPI, List<KeyValuePair<string, string>> postParams) where T : new()
        {
            var result = new T();

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    client.BaseAddress = new Uri(webHCatBaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                    HttpContent content = new FormUrlEncodedContent(postParams); ;
                    HttpResponseMessage response = await client.PostAsync(WebHCatAPI, content);

                    var jsonString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(jsonString);

                    return result;
                }
            }
        }

        protected async Task<T> Put<T>(string webHCatBaseUrl, string webHCatVersion, string webHCatUserName, string WebHCatAPI, object jsonParams) where T : new()
        {
            var result = new T();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    client.BaseAddress = new Uri(webHCatBaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(jsonParams), System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(WebHCatAPI, content);

                    var jsonString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(jsonString);

                    return result;
                }
            }
        }

        protected async Task<T> Delete<T>(string webHCatBaseUrl, string webHCatVersion, string webHCatUserName, string WebHCatAPI, object jsonParams) where T : new()
        {
            var result = new T();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    client.BaseAddress = new Uri(webHCatBaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                    string requestUri = string.Concat(webHCatBaseUrl, WebHCatAPI);
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(jsonParams), System.Text.Encoding.UTF8, "application/json"),
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(requestUri)
                    };
                    HttpResponseMessage response = await client.SendAsync(request);

                    var jsonString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(jsonString);
                    return result;
                }
            }
        }
        #endregion
    }
}
