using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Contracts
{
    public interface IProductsRepository
    {
        Task<ResponseWrapper<IEnumerable<Product>>> GetProducts(IEnumerable<string> productIds);
        Task<ResponseWrapper<Product>> GetProduct(string productId);
    }
}