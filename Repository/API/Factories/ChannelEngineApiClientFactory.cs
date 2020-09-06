using System;
using Contracts.ApiClient;
using Contracts.ApiClient.Factories;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Repository.API.Factories
{
    public class ChannelEngineApiClientFactory : IChannelEngineApiClientFactory
    {
        private readonly Uri _apiUrl;
        public ChannelEngineApiClientFactory(ISharedApiConfigurationProvider configProvider)
        {
            _apiUrl = new Uri($"{configProvider.BaseUri}/{configProvider.ApiVersion}", UriKind.Absolute);
        }

        public IRestClient CreateClient()
        {
            return new RestClient(_apiUrl).UseNewtonsoftJson();
        }
    }
}