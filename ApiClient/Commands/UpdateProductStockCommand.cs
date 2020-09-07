using MediatR;
using Models;

namespace ApiClient.Commands
{
    public class UpdateProductStockCommand : IRequest<Product>
    {
        public UpdateProductStockCommand(TopProductDto product, int stockValue)
        {
            Product = product;
            StockValue = stockValue;
        }

        public TopProductDto Product { get; }
        public int StockValue { get; }
    }
}