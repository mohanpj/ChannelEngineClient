using System.Collections.Generic;
using MediatR;
using Models;

namespace Repository.API.Queries
{
    public class GetTopSoldProductsFromOrdersQuery : IRequest<IEnumerable<TopProductDto>>
    {
        public GetTopSoldProductsFromOrdersQuery(IEnumerable<Order> orders)
        {
            Orders = orders;
        }

        public IEnumerable<Order> Orders { get; }
    }
}