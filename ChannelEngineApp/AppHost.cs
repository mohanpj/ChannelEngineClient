using System;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Microsoft.Extensions.Hosting;

namespace ChannelEngineConsoleApp
{
    public class AppHost : IHostedService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IConsoleMenuService _consoleMenuService;

        public AppHost(IConsoleMenuService consoleMenuService, IHostApplicationLifetime applicationLifetime)
        {
            _consoleMenuService = consoleMenuService;
            _applicationLifetime = applicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Factory.StartNew(RunAsync, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task RunAsync()
        {
            try
            {
                _applicationLifetime.ApplicationStarted.WaitHandle.WaitOne();
                await _consoleMenuService.DrawMenuAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong.");
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine("Press any key to terminate...");
                Console.ReadKey();
            }
            finally
            {
                _applicationLifetime.StopApplication();
            }
        }
    }
}