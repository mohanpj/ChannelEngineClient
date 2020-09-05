using System.IO;

using Contracts;
using Contracts.ApiClient;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Services;
using Services.ApiClient;

namespace Shared
{
    public static class SharedConfigurationExtension
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration config)
        {
            var sharedApiConfig = new SharedApiConfigurationProvider();
            config.GetSection(SharedApiConfigurationProvider.SettingsRoot).Bind(sharedApiConfig);
            services.AddSingleton<ISharedApiConfigurationProvider, SharedApiConfigurationProvider>(provider => sharedApiConfig)
                .AddSingleton<IChannelEngineServiceWrapper, ChannelEngineServiceWrapper>()
                .AddSingleton<IChannelEngineApiClientFactory, ChannelEngineApiClientFactory>()
                .AddTransient<IOrdersService, OrdersService>();

            return services;
        }

        public static IConfigurationBuilder AddSharedConfiguration(this IConfigurationBuilder config,
            IHostEnvironment env)
        {
            var sharedDirectory = Path.Combine(env.ContentRootPath, "..", "Shared");

            config.AddJsonFile(Path.Combine(sharedDirectory, "sharedSettings.json"), true, true)
                .AddJsonFile(Path.Combine(sharedDirectory, $"sharedSettings.{env.EnvironmentName}.json"), true, true)
                .AddJsonFile("sharedSettings.json", true, true)
                .AddJsonFile($"sharedSettings.{env.EnvironmentName}.json", true, true);

            if (env.IsDevelopment())
                config.AddUserSecrets(typeof(SharedApiConfigurationProvider).Assembly);

            return config;
        }
    }
}
