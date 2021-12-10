using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using XPetClinic_MobileFrontend.Views;

namespace XPetClinic_MobileFrontend.Services.Network
{
    public sealed class NetworkService<T> : INetworkService<T> where T : HttpResponseMessage, new()
    {

        private static readonly Lazy<NetworkService<T>> lazy =
           new Lazy<NetworkService<T>>(() => new NetworkService<T>());

        public static NetworkService<T> Instance { get => lazy.Value; }

        private HttpClient httpClient;

        public string JWT { get; set; }

        private NetworkService()
        {
            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(3);
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWT);

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                response = await httpClient.GetAsync(uri);
            }
            catch(OperationCanceledException e)
            {
                response.StatusCode = HttpStatusCode.ServiceUnavailable;
                Console.WriteLine(JWT);
                return response;
            }

            if (response.StatusCode.Equals(HttpStatusCode.Forbidden))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, object body)
        {
            httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(body);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                response =
                await httpClient.PostAsync(uri, content);
            }
            catch(OperationCanceledException e)
            {
                response.StatusCode = HttpStatusCode.ServiceUnavailable;
                return response;
            }

            HttpHeaders headers = response.Headers;
            IEnumerable<string> values;

            if (headers.TryGetValues("Authorization", out values))
            {
                JWT = values.First();
            }

            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(string uri, object body)
        {
            httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(body);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                response =
                await httpClient.PutAsync(uri, content);
            }
            catch (OperationCanceledException e)
            {
                response.StatusCode = HttpStatusCode.ServiceUnavailable;
                return response;
            }

            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                response =
                await httpClient.DeleteAsync(uri);
            }
            catch (OperationCanceledException e)
            {
                response.StatusCode = HttpStatusCode.ServiceUnavailable;
                return response;
            }

            return response;
        }
    }
}
