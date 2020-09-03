using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Contracts;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Services;

namespace ChannelEngineConsoleApp
{
    static class Program
    {
        private static IConfiguration _config;
        private static IServiceProvider _services;
        private static readonly HttpClient HttpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            CreateConfiguration();
            ConfigureServices();
            ConfigureClient();

            await _services.GetService<AppHost>().RunAsync();
        }

        private static void CreateConfiguration()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets(Assembly.GetCallingAssembly())
                .Build();
        }

        private static void ConfigureServices()
        {
            _services = new ServiceCollection()
                .AddScoped<IChannelEngineServiceWrapper, ChannelEngineServiceWrapper>(provider => new ChannelEngineServiceWrapper(HttpClient))
                .AddSingleton<IConsoleMenuService, ConsoleMenuService>()
                .AddSingleton<AppHost>()
                .BuildServiceProvider();
        }

        private static void ConfigureClient()
        {
            HttpClient.BaseAddress = new Uri($"{_config["AppConfig:ApiBaseUri"]}{_config["AppConfig:ApiVersion"]}");
            HttpClient.DefaultRequestHeaders.Add("User-Agent", _config["AppConfig:UserAgent"]);
            HttpClient.DefaultRequestHeaders.Add("X-CE-KEY", _config["ChEng:ApiKey"]);
        }
    }
}
