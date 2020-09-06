using System.Collections.Generic;
using MediatR;
using Models;

namespace Repository.API.Commands
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