using MediatR;
using Models;

namespace Repository.API.Commands
{
    public class UpdateProductStockCommand : IRequest<Product>
    {
        public Product Product { get; set; }

        public UpdateProductStockCommand(Product product)
        {
            Product = product;
        }
    }
}