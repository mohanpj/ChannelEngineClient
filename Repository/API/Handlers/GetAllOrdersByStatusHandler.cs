using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using MediatR;
using Models;
using Repository.API.Queries;

namespace Repository.API.Handlers
{
    public class GetAllOrdersByStatusHandler : IRequestHandler<GetAllOrdersByStatusQuery, IEnumerable<Order>>
    {
        private readonly IChannelEngineRepositoryWrapper _channelEngineRepository;

        public GetAllOrdersByStatusHandler(IChannelEngineRepositoryWrapper channelEngineRepository)
        {
            _channelEngineRepository = channelEngineRepository;
        }

        public async Task<IEnumerable<Order>> Handle(GetAllOrdersByStatusQuery request, CancellationToken cancellationToken)
        {
            var orders = await _channelEngineRepository.OrdersRepository.GetAllOrdersWithStatus(request.Status);
            return orders;
        }
    }
}