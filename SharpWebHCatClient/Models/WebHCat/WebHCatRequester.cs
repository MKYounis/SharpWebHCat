using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TamkeenCommon;

namespace SharpHive.Models.WebHCat
{
    public abstract class WebHCatRequester
    {
        #region Helpers
        protected async Task<T> Get<T>(string WebHCatAPI) where T : new()
        {
            var result = new T();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    client.BaseAddress = new Uri(Configurations.Instance.WebHCatBaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Configurations.Instance.ApplictionType));//ACCEPT header

                    HttpResponseMessage response = await client.GetAsync(WebHCatAPI);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(jsonString);
                }
            }

            return result;
        }

        protected async Task<T> Post<T>(string WebHCatAPI, List<KeyValuePair<string, string>> postParams) where T : new()
        {
            var result = new T();

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    client.BaseAddress = new Uri(Configurations.Instance.WebHCatBaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Configurations.Instance.ApplictionType));//ACCEPT header

                    HttpContent content = new FormUrlEncodedContent(postParams); ;
                    HttpResponseMessage response = await client.PostAsync(WebHCatAPI, content);

                    var jsonString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(jsonString);

                    return result;
                }
            }
        }

        protected async Task<T> Put<T>(string WebHCatAPI, object jsonParams) where T : new()
        {
            var result = new T();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    client.BaseAddress = new Uri(Configurations.Instance.WebHCatBaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Configurations.Instance.ApplictionType));//ACCEPT header

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(jsonParams), System.Text.Encoding.UTF8, Configurations.Instance.ApplictionType);
                    HttpResponseMessage response = await client.PutAsync(WebHCatAPI, content);

                    var jsonString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(jsonString);

                    return result;
                }
            }
        }

        protected async Task<T> Delete<T>(string WebHCatAPI, object jsonParams) where T : new()
        {
            var result = new T();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    client.BaseAddress = new Uri(Configurations.Instance.WebHCatBaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Configurations.Instance.ApplictionType));//ACCEPT header

                    string requestUri = string.Concat(Configurations.Instance.WebHCatBaseURL, WebHCatAPI);
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(jsonParams), System.Text.Encoding.UTF8, Configurations.Instance.ApplictionType),
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
