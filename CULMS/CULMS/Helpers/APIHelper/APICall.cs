using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CULMS.Helpers.APIHelper
{
    public class APICall
    {
        #region API call
        public async Task<T> GetRequestAsync<T>(string url, bool tokenrequired) where T : new()
        {
            var obj = new T();
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    var uri = new Uri($"{StringConstant.ApiKeyUrl}{url}");
                    using (var httpClient = new HttpClient())
                    {
                        if (tokenrequired)
                        {
                            httpClient.DefaultRequestHeaders.Clear();
                            httpClient.DefaultRequestHeaders.Add(StringConstant.ApiKeyHttpAuthorization, string.Concat(StringConstant.ApiKeyBearer, Preferences.Get("Token", null)));
                        }
                        var response = await httpClient.GetAsync(uri);
                        if (response.IsSuccessStatusCode)
                        {
                            obj = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                        }
                        else
                        {
                            obj = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                            switch (response.StatusCode)
                            {
                                case HttpStatusCode.Unauthorized:
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Internet connection error", "Ok");
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return obj;
        }

        public async Task<HttpResponseMessage> PostRequest(string serviceUrl, string jsonRequest)
        {
            HttpResponseMessage responseData = null;
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    using (var httpClient = new HttpClient())
                    {
                        var uri = new Uri(string.Format(serviceUrl, string.Empty));
                        HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                        responseData = httpClient.PostAsync(uri, content).GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseData;
        }
        public async Task<HttpResponseMessage> PostRequestToken(string serviceUrl, string jsonRequest, string token)
        {
            HttpResponseMessage responseData = null;
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        var uri = new Uri(string.Format(serviceUrl, string.Empty));
                        HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                        responseData = httpClient.PostAsync(uri, content).GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseData;
        }
        public async Task<HttpResponseMessage> GetRequest(string serviceUrl, string token)
        {
            HttpResponseMessage responseData = null;
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        var uri = new Uri(string.Format(serviceUrl, string.Empty));
                        httpClient.Timeout = TimeSpan.FromSeconds(200);
                        var response = httpClient.GetAsync(uri).GetAwaiter().GetResult();
                        if (response.IsSuccessStatusCode)
                        {
                            responseData = response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LogManager.Savelog(nameof(LocalGetRequest), ex);
            }
            return responseData;
        }
        #endregion
    }
}
