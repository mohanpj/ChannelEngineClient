using RestSharp;

namespace Contracts.ApiClient
{
    public interface IChannelEngineApiClientFactory
    {
        IRestClient CreateClient();
    }
}