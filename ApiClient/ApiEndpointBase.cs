﻿using System.Net;
using ApiClient.Extensions;
using Contracts.ApiClient;
using Contracts.ApiClient.Factories;
using RestSharp;

namespace ApiClient
{
    public abstract class ApiEndpointBase
    {
        protected readonly IChannelEngineApiClientFactory ClientFactory;
        protected readonly IChannelEngineApiRequestFactory RequestFactory;
        protected readonly ISharedSettingsProvider SharedConfig;

        protected ApiEndpointBase(
            IChannelEngineApiClientFactory clientFactory,
            IChannelEngineApiRequestFactory requestFactory,
            ISharedSettingsProvider sharedConfig)
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
                        "Could not communicate with service",
                        new
                        {
                            ResponseContent = response.Content,
                            ResponseStatus = response.StatusCode,
                            response.Request.Resource
                        });
            }
        }
    }
}