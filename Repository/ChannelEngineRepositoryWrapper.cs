using Contracts.Repository;

namespace Repository
{
    public class ChannelEngineRepositoryWrapper : IChannelEngineRepositoryWrapper
    {
        public ChannelEngineRepositoryWrapper(IOrdersRepository ordersRepository,
            IProductsRepository productsRepository)
        {
            Orders = ordersRepository;
            Products = productsRepository;
        }

        public IOrdersRepository Orders { get; }
        public IProductsRepository Products { get; }
    }
}