using Contracts;

using Microsoft.Extensions.Configuration;

using RestSharp;

namespace Services
{
    public class ChannelEngineServiceWrapper : IChannelEngineServiceWrapper
    {
        private readonly IConfiguration _config;
        private readonly IRestClient _httpClient;

        public IOrdersService Orders { get; }

        public ChannelEngineServiceWrapper(
            IRestClient httpClient,
            IConfiguration config,
            IOrdersService orders)
        {
            _httpClient = httpClient;
            _config = config;
            Orders = orders;
        }
    }
}