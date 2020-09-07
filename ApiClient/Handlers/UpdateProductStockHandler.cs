using System.Threading;
using System.Threading.Tasks;
using ApiClient.Commands;
using Contracts.Repository;
using MediatR;
using Models;

namespace ApiClient.Handlers
{
    public class UpdateProductStockHandler : IRequestHandler<UpdateProductStockCommand, Product>
    {
        private readonly IChannelEngineRepositoryWrapper _repository;

        public UpdateProductStockHandler(IChannelEngineRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<Product> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.Products.GetProduct(request.Product.MerchantProductNo);
            var updatedProduct = await _repository.Products.UpdateProduct(product);
            return updatedProduct;
        }
    }
}