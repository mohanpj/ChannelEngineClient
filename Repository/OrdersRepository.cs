using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiClient;
using Contracts.ApiClient;
using Contracts.ApiClient.Factories;
using Contracts.Repository;
using Models;

namespace Repository
{
    public class OrdersRepository : ApiEndpointBase, IOrdersRepository
    {
        private const string StatusesParam = "statuses";

        public OrdersRepository(
            IChannelEngineApiRequestFactory requestFactory,
            IChannelEngineApiClientFactory clientFactory,
            ISharedSettingsProvider configurationProvider)
            : base(clientFactory, requestFactory, configurationProvider)
        {
        }

        public async Task<IEnumerable<Order>> GetAllOrdersWithStatus(OrderStatus status)
        {
            var statusName = Enum.GetName(typeof(OrderStatus), status);
            var request = RequestFactory
                .CreateRequest(SharedConfig.OrdersEndpoint);

            if (statusName != null && status != OrderStatus.NONE)
            {
                request.AddQueryParameter(StatusesParam, statusName);
            }

            var response = await ClientFactory
                .CreateClient()
                .ExecuteGetAsync<ResponseWrapper<IEnumerable<Order>>>(request);
            EnsureSuccess(response);
            return response.Data.Content;
        }
    }
}