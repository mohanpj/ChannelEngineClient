using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Contracts.Repository
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersWithStatus(OrderStatus status);
    }
}