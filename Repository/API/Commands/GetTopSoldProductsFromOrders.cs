using System.Collections.Generic;
using MediatR;
using Models;

namespace Repository.API.Commands
{
    public class GetTopSoldProductsFromOrders : IRequest<ResponseWrapper<IEnumerable<Product>>>
    {
        public GetTopSoldProductsFromOrders(IEnumerable<string> productIds)
        {
            ProductIds = productIds;
        }

        public IEnumerable<string> ProductIds { get; }
    }
}