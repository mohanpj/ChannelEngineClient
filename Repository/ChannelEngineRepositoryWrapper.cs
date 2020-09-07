using Contracts.Repository;

namespace Repository
{
    public class ChannelEngineRepositoryWrapper : IChannelEngineRepositoryWrapper
    {
        public ChannelEngineRepositoryWrapper(IOrdersRepository ordersRepository,
            IProductsRepository productsRepository)
        {
            OrdersRepository = ordersRepository;
            Products = productsRepository;
        }

        public IOrdersRepository OrdersRepository { get; }
        public IProductsRepository Products { get; }
    }
}