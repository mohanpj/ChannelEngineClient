using System.Collections.Generic;
using MediatR;
using Models;

namespace ApiClient.Queries
{
    public class GetTopSoldProductsFromOrdersQuery : IRequest<IEnumerable<TopProductDto>>
    {
        public GetTopSoldProductsFromOrdersQuery(IEnumerable<Order> orders, int count)
        {
            Orders = orders;
            Count = count;
        }

        public IEnumerable<Order> Orders { get; }
        public int Count { get; }
    }
}