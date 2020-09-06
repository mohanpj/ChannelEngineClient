using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Contracts;
using MediatR;
using Models;

namespace Services.ApiClient.Commands
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