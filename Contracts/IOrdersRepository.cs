using System.Threading.Tasks;
using Models;

namespace Contracts
{
    public interface IOrdersRepository
    {
        Task<ResponseWrapper<Order>> GetAllOrdersWithStatus(OrderStatus status);
    }
}