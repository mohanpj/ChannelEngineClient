using System.Net;
using Contracts.ApiClient;
using Contracts.ApiClient.Factories;
using Repository.API.Extensions;
using RestSharp;

namespace Repository.API
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

        protected virtual void EnsureSuccess(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
                HandleResponseException(response);
        }

        private void HandleResponseException(IRestResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new ChannelEngineApiClientException(
                        HttpStatusCode.InternalServerError,
                        $"Could not find resource under {response.Request.Resource}",
                        new {ResponseContent = response.Content});
                case HttpStatusCode.BadRequest:
                    throw new ChannelEngineApiClientException(
                        HttpStatusCode.BadRequest,
                        $"Could not process request for resource {response.Request.Resource}",
                        new
                        {
                            ResponseContent = response.Content,
                            ResponseMessage = response.ErrorMessage
                        });
                case HttpStatusCode.ServiceUnavailable:
                    throw new ChannelEngineApiClientException(
                        HttpStatusCode.ServiceUnavailable,
                        "Connection with Channel Engine API failed.",
                        new {response.Request.Resource});
                default:
                    throw new ChannelEngineApiClientException(
                        HttpStatusCode.BadGateway,
                        $"Could not communicate with service",
                        new
                        {
                            ResponseContent = response.Content,
                            ResponseStatus = response.StatusCode,
                            Resource = response.Request.Resource
                        });
            }
        }
    }
}