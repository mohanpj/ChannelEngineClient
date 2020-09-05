using System.Threading.Tasks;
using Models;

namespace Contracts
{
    public interface IOrdersService
    {
        Task<ResponseWrapper<Order>> GetAllWithStatusAsync(OrderStatus status);
    }
}