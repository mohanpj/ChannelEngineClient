using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Contracts
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersWithStatus(OrderStatus status);
    }
}