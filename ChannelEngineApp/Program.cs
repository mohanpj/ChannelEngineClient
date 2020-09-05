using System.Threading.Tasks;
using Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using Shared;

namespace ChannelEngineConsoleApp
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(CreateConfiguration)
                .ConfigureServices(RegisterServices);
        }

        private static void CreateConfiguration(HostBuilderContext hostContext, IConfigurationBuilder config)
        {
            var env = hostContext.HostingEnvironment;
            config
                .AddEnvironmentVariables("DOTNET_")
                .AddSharedConfiguration(env)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
        }

        private static void RegisterServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddSharedServices(hostContext.Configuration)
                .AddSingleton<IConsoleMenuService, ConsoleMenuService>()
                .AddHostedService<AppHost>();
        }
    }
}