using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XPetClinic_MobileFrontend.Services.Network
{
    public interface INetworkService<T> where T : HttpResponseMessage, new()
    {
        Task<HttpResponseMessage> GetAsync(string uri);
        Task<HttpResponseMessage> PostAsync(string uri, object body);
        Task<HttpResponseMessage> PutAsync(string uri, object body);
        Task<HttpResponseMessage> DeleteAsync(string uri);
    }
}
