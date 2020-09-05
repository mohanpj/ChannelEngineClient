using System.Threading.Tasks;
using Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Services;

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
            config.AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddUserSecrets(typeof(Program).Assembly);
        }

        private static void RegisterServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            var config = hostContext.Configuration;
            services
                .AddSingleton<IConsoleMenuService, ConsoleMenuService>()
                .AddSingleton(provider =>
                    new RestClient($"{config["AppConfig:ApiBaseUri"]}{config["AppConfig:ApiVersion"]}")
                        .AddDefaultHeader("User-Agent", config["AppConfig:UserAgent"])
                        .AddDefaultHeader("X-CE-KEY", config["ChEng:ApiKey"])
                        .UseNewtonsoftJson())
                .AddTransient<IOrdersService, OrdersService>()
                .AddSingleton<IChannelEngineServiceWrapper, ChannelEngineServiceWrapper>()
                .AddHostedService<AppHost>();
        }
    }
}