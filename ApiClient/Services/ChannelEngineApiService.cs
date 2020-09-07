using System.Collections.Generic;
using System.Threading.Tasks;
using ApiClient.Commands;
using ApiClient.Queries;
using Contracts.ApiClient;
using MediatR;
using Models;

namespace ApiClient.Services
{
    public class ChannelEngineApiService : IChannelEngineApiService
    {
        private readonly IMediator _mediator;

        public ChannelEngineApiService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<Order>> GetOrdersWithStatus(OrderStatus status)
        {
            var query = new GetAllOrdersByStatusQuery(status);
            return await _mediator.Send(query);
        }

        public async Task<IEnumerable<TopProductDto>> GetTopSoldProductsFromOrders(IEnumerable<Order> orders, int count = 5)
        {
            var query = new GetTopSoldProductsFromOrdersQuery(orders, count);
            return await _mediator.Send(query);
        }

        public async Task<IEnumerable<Product>> GetProducts(string[] productIds)
        {
            var query = new GetProductsByIdsQuery(productIds);
            return await _mediator.Send(query);
        }

        public async Task<Product> UpdateProductStock(TopProductDto product, int stock = 25)
        {
            var command = new UpdateProductStockCommand(product, stock);
            return await _mediator.Send(command);
        }
    }
}