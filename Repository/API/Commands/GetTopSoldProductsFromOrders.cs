using System.Collections.Generic;
using MediatR;
using Models;

namespace Repository.API.Commands
{
    public class GetTopSoldProductsFromOrders : IRequest<IEnumerable<TopProductDto>>
    {
        public GetTopSoldProductsFromOrders(IEnumerable<Order> orders)
        {
            Orders = orders;
        }

        public IEnumerable<Order> Orders { get; }
    }
}