﻿namespace Contracts.ApiClient
{
    public interface ISharedApiConfigurationProvider
    {
        string BaseUri { get; set; }
        string ApiVersion { get; set; }
        string ApiToken { get; set; }
        string OrdersEndpoint { get; set; }
        string ProductsEndpoint { get; set; }
    }
}