﻿using System.Threading.Tasks;
using ChannelEngineConsoleApp.Services;
using Contracts.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            config.AddEnvironmentVariables("DOTNET_")
                .AddSharedConfiguration(env)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
        }

        private static void RegisterServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddSingleton<IConsolePrintingService, ConsolePrintingService>()
                .AddSingleton<IConsoleMenuService, ConsoleMenuService>()
                .AddSharedServices(hostContext.Configuration)
                .AddHostedService<AppHost>();
        }
    }
}