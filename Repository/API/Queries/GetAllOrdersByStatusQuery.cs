using System.Collections.Generic;
using MediatR;
using Models;

namespace Repository.API.Queries
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