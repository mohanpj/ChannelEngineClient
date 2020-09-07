using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiClient.Queries;
using Contracts.Repository;
using MediatR;
using Models;

namespace ApiClient.Handlers
{
    public class GetProductsByIdsHandler : IRequestHandler<GetProductsByIdsQuery, IEnumerable<Product>>
    {
        private readonly IChannelEngineRepositoryWrapper _repository;

        public GetProductsByIdsHandler(IChannelEngineRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsByIdsQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.Products.GetProductsByMerchantNo(request.ProductIds);
            return response;
        }
    }
}