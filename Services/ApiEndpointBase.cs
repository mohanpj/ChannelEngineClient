using Contracts.ApiClient;
using Contracts.ApiClient.Factories;

namespace Services
{
    public abstract class ApiEndpointBase
    {
        protected readonly IChannelEngineApiClientFactory ClientFactory;
        protected readonly IChannelEngineApiRequestFactory RequestFactory;
        protected readonly ISharedApiConfigurationProvider SharedConfig;

        protected ApiEndpointBase(
            IChannelEngineApiClientFactory clientFactory,
            IChannelEngineApiRequestFactory requestFactory,
            ISharedApiConfigurationProvider sharedConfig)
        {
            ClientFactory = clientFactory;
            RequestFactory = requestFactory;
            SharedConfig = sharedConfig;
        }
    }
}