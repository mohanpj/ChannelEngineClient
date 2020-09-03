using System.Net.Http;

using Contracts;

using Microsoft.Extensions.Configuration;

namespace Services
{
    public class ChannelEngineServiceWrapper : IChannelEngineServiceWrapper
    {
        private readonly HttpClient _httpClient;
        private IOrdersService _orders;

        public ChannelEngineServiceWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IOrdersService Orders => _orders ??= new OrdersService(_httpClient);
    }
}