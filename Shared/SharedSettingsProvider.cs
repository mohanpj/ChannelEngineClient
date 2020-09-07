using Contracts.ApiClient;

namespace Shared
{
    public class SharedSettingsProvider : ISharedSettingsProvider
    {
        public const string SettingsRoot = "ApiConfig";
        public string BaseUri { get; set; }
        public string ApiVersion { get; set; }
        public string ApiToken { get; set; }
        public string OrdersEndpoint { get; set; }
        public string ProductsEndpoint { get; set; }
    }
}