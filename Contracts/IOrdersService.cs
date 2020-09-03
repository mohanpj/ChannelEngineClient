using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Contracts
{
    public interface IOrdersService
    {
        Task<IEnumerable<Order>> GetAllWithStatusAsync(string status);
    }
}