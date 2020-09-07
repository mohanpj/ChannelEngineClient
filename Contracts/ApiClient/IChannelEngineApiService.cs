using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Contracts.ApiClient
{
    public interface IChannelEngineApiService
    {
        Task<IEnumerable<Order>> GetOrdersWithStatus(OrderStatus status);
        Task<IEnumerable<TopProductDto>> GetTopSoldProductsFromOrders(IEnumerable<Order> orders, int count = 5);
        Task<IEnumerable<Product>> GetProducts(string[] productIds);
        Task<Product> UpdateProductStock(TopProductDto product, int stock = 25);
    }
}