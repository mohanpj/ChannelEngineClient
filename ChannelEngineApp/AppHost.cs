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

        public void Run()
        {
            _consoleMenuService.DrawMenu();
        }
    }
}