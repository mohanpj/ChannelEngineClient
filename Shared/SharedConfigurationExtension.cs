using System.IO;
using ApiClient.Factories;
using ApiClient.Queries;
using ApiClient.Services;
using Contracts.ApiClient;
using Contracts.ApiClient.Factories;
using Contracts.Repository;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;

namespace Shared
{
    public static class SharedConfigurationExtension
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration config)
        {
            var settings = new SharedSettingsProvider();
            services.AddMediatR(typeof(GetProductsByIdsQuery).Assembly);
            config.GetSection(SharedSettingsProvider.SettingsRoot).Bind(settings);
            services.AddSingleton<ISharedSettingsProvider, SharedSettingsProvider>(provider => settings)
                .AddRepositories()
                .AddFactories()
                .AddSingleton<IChannelEngineApiService, ChannelEngineApiService>();

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
                config.AddUserSecrets(typeof(SharedSettingsProvider).Assembly);

            return config;
        }

        private static IServiceCollection AddFactories(this IServiceCollection services)
        {
            return services.AddSingleton<IChannelEngineApiClientFactory, ChannelEngineApiClientFactory>()
                .AddSingleton<IChannelEngineApiRequestFactory, ChannelEngineApiRequestFactory>();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddTransient<IOrdersRepository, OrdersRepository>()
                .AddTransient<IProductsRepository, ProductsRepository>()
                .AddSingleton<IChannelEngineRepositoryWrapper, ChannelEngineRepositoryWrapper>();
        }
    }
}