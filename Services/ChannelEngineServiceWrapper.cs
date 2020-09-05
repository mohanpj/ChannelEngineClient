using Contracts;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Services
{
    public class ChannelEngineServiceWrapper : IChannelEngineServiceWrapper
    {
        private readonly IConfiguration _config;
        private readonly IRestClient _httpClient;
        private IOrdersService _orders;

        public ChannelEngineServiceWrapper(IRestClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public IOrdersService Orders => _orders ??= new OrdersService(_httpClient, _config);
    }
}