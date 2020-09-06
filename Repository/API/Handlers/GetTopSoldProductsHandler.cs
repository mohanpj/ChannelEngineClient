using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using MediatR;
using Models;
using Repository.API.Commands;

namespace Repository.API.Handlers
{
    public class GetTopSoldProductsHandler : IRequestHandler<GetTopSoldProductsFromOrders, ResponseWrapper<IEnumerable<Product>>>
    {
        private readonly IChannelEngineRepositoryWrapper _repository;

        public GetTopSoldProductsHandler(IChannelEngineRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<ResponseWrapper<IEnumerable<Product>>> Handle(GetTopSoldProductsFromOrders request, CancellationToken cancellationToken)
        {
            var products = await _repository.Products.GetProducts(request.ProductIds);
            
            return products;
        }
    }
}