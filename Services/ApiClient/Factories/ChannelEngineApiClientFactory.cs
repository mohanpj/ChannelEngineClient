using Contracts.ApiClient;
using Contracts.ApiClient.Factories;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Services.ApiClient.Factories
{
    public class ChannelEngineApiClientFactory : IChannelEngineApiClientFactory
    {
        private readonly string _apiUrl;
        public ChannelEngineApiClientFactory(ISharedApiConfigurationProvider configProvider)
        {
            _apiUrl = $"{configProvider.BaseUri}/{configProvider.ApiVersion}";
        }

        public IRestClient CreateClient()
        {
            return new RestClient(_apiUrl).UseNewtonsoftJson();
        }
    }
}