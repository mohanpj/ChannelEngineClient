using Contracts.ApiClient;
using RestSharp;

namespace Services
{
    public abstract class ApiEndpointBase
    {
        protected readonly IRestClient Client;

        protected ApiEndpointBase(IChannelEngineApiClientFactory apiClientFactory)
        {
            Client = apiClientFactory.CreateClient();
        }

        protected virtual IRestRequest CreateRequest()
        {
            return new RestRequest();
        }
    }
}