using Contracts;

namespace Repository
{
    public class ChannelEngineRepositoryWrapper : IChannelEngineRepositoryWrapper
    {
        public IOrdersRepository Orders { get; }

        public ChannelEngineRepositoryWrapper(IOrdersRepository orders)
        {
            Orders = orders;
        }
    }
}