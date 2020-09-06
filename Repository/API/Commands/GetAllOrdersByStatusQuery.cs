using MediatR;
using Models;

namespace Repository.API.Commands
{
    public class GetAllOrdersByStatusQuery : IRequest<ResponseWrapper<Order>>
    {
        public GetAllOrdersByStatusQuery(OrderStatus status)
        {
            Status = status;
        }

        public OrderStatus Status { get; }
    }
}