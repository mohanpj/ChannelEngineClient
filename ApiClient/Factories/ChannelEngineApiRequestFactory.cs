using Contracts.ApiClient;
using Contracts.ApiClient.Factories;
using RestSharp;

namespace ApiClient.Factories
{
    public class ChannelEngineApiRequestFactory : IChannelEngineApiRequestFactory
    {
        private readonly ISharedSettingsProvider _sharedSettings;

        public ChannelEngineApiRequestFactory(ISharedSettingsProvider sharedSettings)
        {
            _sharedSettings = sharedSettings;
        }

        public IRestRequest CreateRequest(string resource)
        {
            return new RestRequest(resource)
                .AddHeader("X-CE-KEY", _sharedSettings.ApiToken);
        }
    }
}