using Contracts;
using Contracts.Repository;

namespace Repository
{
    public class ChannelEngineRepositoryWrapper : IChannelEngineRepositoryWrapper
    {
        public IOrdersRepository OrdersRepository { get; }
        public IProductsRepository Products { get; }

        public ChannelEngineRepositoryWrapper(IOrdersRepository ordersRepository,
            IProductsRepository productsRepository)
        {
            OrdersRepository = ordersRepository;
            Products = productsRepository;
        }
    }
}