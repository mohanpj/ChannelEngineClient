using System.Threading;
using System.Threading.Tasks;
using Contracts;
using MediatR;
using Models;
using Repository.API.Commands;

namespace Repository.API.Handlers
{
    public class GetAllOrdersByStatusHandler : IRequestHandler<GetAllOrdersByStatusQuery, ResponseWrapper<Order>>
    {
        private readonly IChannelEngineRepositoryWrapper _channelEngineRepository;

        public GetAllOrdersByStatusHandler(IChannelEngineRepositoryWrapper channelEngineRepository)
        {
            _channelEngineRepository = channelEngineRepository;
        }

        public async Task<ResponseWrapper<Order>> Handle(GetAllOrdersByStatusQuery request, CancellationToken cancellationToken)
        {
            var orders = await _channelEngineRepository.Orders.GetAllOrdersWithStatus(request.Status);
            return orders;
        }
    }
}