namespace Contracts
{
    public interface IChannelEngineRepositoryWrapper
    {
        public IOrdersRepository OrdersRepository { get; }
        public IProductsRepository Products { get; }
    }
}