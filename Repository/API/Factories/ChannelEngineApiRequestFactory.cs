using Contracts.ApiClient;
using Contracts.ApiClient.Factories;
using RestSharp;

namespace Repository.API.Factories
{
    public class ChannelEngineApiRequestFactory : IChannelEngineApiRequestFactory
    {
        private readonly ISharedApiConfigurationProvider _sharedApiConfiguration;

        public ChannelEngineApiRequestFactory(ISharedApiConfigurationProvider sharedApiConfiguration)
        {
            _sharedApiConfiguration = sharedApiConfiguration;
        }

        public IRestRequest CreateRequest(string resource)
        {
            return new RestRequest(resource)
                .AddHeader("X-CE-KEY", _sharedApiConfiguration.ApiToken);
        }
    }
}