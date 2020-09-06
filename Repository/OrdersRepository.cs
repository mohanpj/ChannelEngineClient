using System;
using System.Threading.Tasks;
using Contracts;
using Contracts.ApiClient;
using Contracts.ApiClient.Factories;
using Models;
using RestSharp;

namespace Repository
{
    public class OrdersRepository : ApiEndpointBase, IOrdersRepository
    {
        public OrdersRepository(
            IChannelEngineApiRequestFactory requestFactory,
            IChannelEngineApiClientFactory clientFactory,
            ISharedApiConfigurationProvider configurationProvider)
            :base(clientFactory, requestFactory, configurationProvider)
        {
        }

        public async Task<ResponseWrapper<Order>> GetAllOrdersWithStatus(OrderStatus status)
        {
            var statusName = Enum.GetName(typeof(OrderStatus), status);
            var request = RequestFactory
                .CreateRequest(SharedConfig.OrdersEndpoint);

            if (statusName != null && status != OrderStatus.NONE)
            {
                request.AddQueryParameter("statuses", statusName);
            }

            var response = await ClientFactory
                .CreateClient()
                .GetAsync<ResponseWrapper<Order>>(request);
            
            return response;
        }
    }
}