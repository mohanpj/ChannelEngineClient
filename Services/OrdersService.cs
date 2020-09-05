using System;
using System.Threading.Tasks;

using Contracts;
using Contracts.ApiClient;
using Models;

using RestSharp;

namespace Services
{
    public class OrdersService : ApiEndpointBase, IOrdersService
    {
        private readonly ISharedApiConfigurationProvider _sharedConfig;

        public OrdersService(IChannelEngineApiClientFactory clientFactory, ISharedApiConfigurationProvider sharedConfig)
            :base(clientFactory)
        {
            _sharedConfig = sharedConfig;
        }

        public async Task<ResponseWrapper<Order>> GetAllWithStatusAsync(OrderStatus status)
        {
            var statusName = Enum.GetName(typeof(OrderStatus), status);

            var request = CreateRequest();

            if (statusName != null && status != OrderStatus.NONE)
            {
                request.AddQueryParameter("statuses", statusName);
            }

            var response = await Client.GetAsync<ResponseWrapper<Order>>(request);

            return response;
        }

        protected override IRestRequest CreateRequest()
        {
            return new RestRequest(_sharedConfig.OrdersEndpoint)
                .AddHeader("X-CE-KEY", _sharedConfig.ApiToken);
        }
    }
}