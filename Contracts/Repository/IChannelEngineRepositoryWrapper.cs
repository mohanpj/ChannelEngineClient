namespace Contracts.Repository
{
    public interface IChannelEngineRepositoryWrapper
    {
        public IOrdersRepository Orders { get; }
        public IProductsRepository Products { get; }
    }
}