using RestSharp;

namespace Contracts.ApiClient.Factories
{
    public interface IChannelEngineApiRequestFactory
    {
        IRestRequest CreateRequest(string resource);
    }
}