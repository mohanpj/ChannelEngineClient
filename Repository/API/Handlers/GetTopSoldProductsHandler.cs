﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using MediatR;
using Models;
using Repository.API.Commands;
using RestSharp.Validation;

namespace Repository.API.Handlers
{
    public class GetTopSoldProductsHandler : IRequestHandler<GetTopSoldProductsFromOrders, IEnumerable<TopProductDto>>
    {
        private readonly IChannelEngineRepositoryWrapper _repository;

        public GetTopSoldProductsHandler(IChannelEngineRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TopProductDto>> Handle(GetTopSoldProductsFromOrders request, CancellationToken cancellationToken)
        {
            var productIds = request.Orders.SelectMany(o => o.Lines)
                .Select(l => l.MerchantProductNo)
                .Distinct();
            
            var products = await _repository.Products.GetProductsByMerchantNo(productIds);

            var quantityAggregate = request.Orders.SelectMany(o => o.Lines)
                .Aggregate(new Dictionary<string, int>(), AggregateQuantityByProduct)
                .Take(5)
                .ToArray();

            var result = products.Content
                .Join(quantityAggregate,
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