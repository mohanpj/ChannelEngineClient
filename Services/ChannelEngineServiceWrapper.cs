using Contracts;

namespace Services
{
    public class ChannelEngineServiceWrapper : IChannelEngineServiceWrapper
    {
        public IOrdersService Orders { get; }

        public ChannelEngineServiceWrapper(
            IOrdersService orders)
        {
            Orders = orders;
        }
    }
}