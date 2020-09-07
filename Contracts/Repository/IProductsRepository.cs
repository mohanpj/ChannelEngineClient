using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Contracts.Repository
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetProductsByMerchantNo(IEnumerable<string> productIds);
        Task<Product> GetProduct(string productId);
        Task<Product> UpdateProduct(Product product, int stock);
    }
}