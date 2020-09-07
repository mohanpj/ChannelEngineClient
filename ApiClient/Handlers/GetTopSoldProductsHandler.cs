using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiClient.Queries;
using Contracts.Repository;
using MediatR;
using Models;

namespace ApiClient.Handlers
{
    public class
        GetTopSoldProductsHandler : IRequestHandler<GetTopSoldProductsFromOrdersQuery, IEnumerable<TopProductDto>>
    {
        private readonly IChannelEngineRepositoryWrapper _repository;

        public GetTopSoldProductsHandler(IChannelEngineRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TopProductDto>> Handle(GetTopSoldProductsFromOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var productIds = request.Orders.SelectMany(o => o.Lines)
                .Select(l => l.MerchantProductNo)
                .Distinct();

            var products = await _repository.Products.GetProductsByMerchantNo(productIds);

            var quantityAggregate = request.Orders.SelectMany(o => o.Lines)
                .Aggregate(new Dictionary<string, int>(), AggregateQuantityByProduct)
                .Take(request.Count)
                .ToArray();

            var result = products.Join(quantityAggregate,
                    product => product.MerchantProductNo,
                    qa => qa.Key,
                    (product, qa) => new TopProductDto(product, qa.Value))
                .OrderByDescending(p => p.TotalSold)
                .ThenBy(p => p.Name);

            return result;
        }

        private Dictionary<string, int> AggregateQuantityByProduct(Dictionary<string, int> dict, ProductLine product)
        {
            if (dict.ContainsKey(product.MerchantProductNo))
                dict[product.MerchantProductNo] += product.Quantity;
            else
                dict[product.MerchantProductNo] = product.Quantity;

            return dict;
        }
    }
}