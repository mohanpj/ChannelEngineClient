using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Contracts.Repository;
using MediatR;
using Models;
using Repository.API.Commands;

namespace Repository.API.Handlers
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
            var product = await _repository.Products.UpdateProduct(request.Product);
            return product;
        }
    }
}