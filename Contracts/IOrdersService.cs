using System.Threading.Tasks;
using Models;

namespace Contracts
{
    public interface IOrdersService
    {
        Task<ResponseWrapper<Order>> GetAllOrdersWithStatus(OrderStatus status);
    }
}