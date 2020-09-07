using System.Collections.Generic;
using MediatR;
using Models;

namespace ApiClient.Queries
{
    public class GetAllOrdersByStatusQuery : IRequest<IEnumerable<Order>>
    {
        public GetAllOrdersByStatusQuery(OrderStatus status)
        {
            Status = status;
        }

        public OrderStatus Status { get; }
    }
}