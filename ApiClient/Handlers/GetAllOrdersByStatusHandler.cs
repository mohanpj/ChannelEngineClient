using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiClient.Queries;
using Contracts.Repository;
using MediatR;
using Models;

namespace ApiClient.Handlers
{
    public class GetAllOrdersByStatusHandler : IRequestHandler<GetAllOrdersByStatusQuery, IEnumerable<Order>>
    {
        private readonly IChannelEngineRepositoryWrapper _channelEngineRepository;

        public GetAllOrdersByStatusHandler(IChannelEngineRepositoryWrapper channelEngineRepository)
        {
            _channelEngineRepository = channelEngineRepository;
        }

        public async Task<IEnumerable<Order>> Handle(GetAllOrdersByStatusQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await _channelEngineRepository.Orders.GetAllOrdersWithStatus(request.Status);
            return orders;
        }
    }
}