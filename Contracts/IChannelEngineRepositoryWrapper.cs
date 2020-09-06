namespace Contracts
{
    public interface IChannelEngineRepositoryWrapper
    {
        public IOrdersRepository Orders { get; }
    }
}