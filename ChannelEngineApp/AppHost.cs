using System;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Console;
using Microsoft.Extensions.Hosting;

namespace ChannelEngineConsoleApp
{
    public class AppHost : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IConsoleMenuService _consoleService;
        private readonly IConsolePrintingService _printingService;

        public AppHost(IConsoleMenuService consoleService,
            IConsolePrintingService printingService,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            _consoleService = consoleService;
            _printingService = printingService;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Factory.StartNew(DoWork, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task DoWork()
        {
            try
            {
                _hostApplicationLifetime.ApplicationStarted.WaitHandle.WaitOne();
                await _consoleService.RunAsync();
            }
            catch (Exception ex)
            {
                _printingService.WriteLine($"ERROR: {ex.Message}");
                _printingService.WriteLine($"StackTrace: {ex.StackTrace}");
                _printingService.WriteLine("Press aby key to terminate...");
                Console.ReadKey();
            }
            finally
            {
                _hostApplicationLifetime.StopApplication();
            }
        }
    }
}