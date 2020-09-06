using RestSharp;

namespace Contracts.ApiClient.Factories
{
    public interface IChannelEngineApiClientFactory
    {
        IRestClient CreateClient();
    }
}