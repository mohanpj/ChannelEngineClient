using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using MediatR;
using Models;
using Services.ApiClient.Commands;

namespace Services.ApiClient.Handlers
{
    public class GetAllOrdersByStatusHandler : IRequestHandler<GetAllOrdersByStatusQuery, ResponseWrapper<Order>>
    {
        private readonly IChannelEngineServiceWrapper _channelEngineService;

        public GetAllOrdersByStatusHandler(IChannelEngineServiceWrapper channelEngineService)
        {
            _channelEngineService = channelEngineService;
        }

        public async Task<ResponseWrapper<Order>> Handle(GetAllOrdersByStatusQuery request, CancellationToken cancellationToken)
        {
            var orders = await _channelEngineService.Orders.GetAllOrdersWithStatus(request.Status);
            return orders;
        }
    }
}