using System.Threading.Tasks;
using Contracts;

namespace ChannelEngineConsoleApp
{
    public class AppHost
    {
        private readonly IConsoleMenuService _consoleMenuService;

        public AppHost(IConsoleMenuService consoleMenuService)
        {
            _consoleMenuService = consoleMenuService;
        }

        public async Task RunAsync()
        {
            await _consoleMenuService.DrawMenuAsync();
        }
    }
}