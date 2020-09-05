using System;
using System.Threading.Tasks;
using Contracts;
using Microsoft.Extensions.Configuration;
using Models;
using RestSharp;

namespace Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IConfiguration _config;
        private readonly IRestClient _restClient;

        public OrdersService(IRestClient restClient, IConfiguration config)
        {
            _restClient = restClient;
            _config = config;
        }

        public async Task<ResponseWrapper<Order>> GetAllWithStatusAsync(OrderStatus status)
        {
            var statusName = Enum.GetName(typeof(OrderStatus), status);
            var request = new RestRequest(_config["ApiConfig:OrdersEndpoint"]);
            request.AddQueryParameter("statuses", statusName);

            var response = await _restClient.GetAsync<ResponseWrapper<Order>>(request);

            return response;
        }
    }
}