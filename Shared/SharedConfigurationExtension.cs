using System.IO;

using Contracts;
using Contracts.ApiClient;
using Contracts.ApiClient.Factories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Repository;
using Repository.API.Commands;
using Repository.API.Factories;
using Repository.API.Handlers;

namespace Shared
{
    public static class SharedConfigurationExtension
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration config)
        {
            var sharedApiConfig = new SharedApiConfigurationProvider();
            config.GetSection(SharedApiConfigurationProvider.SettingsRoot).Bind(sharedApiConfig);
            services.AddMediatR(typeof(GetAllOrdersByStatusQuery).Assembly)
                .AddSingleton<ISharedApiConfigurationProvider, SharedApiConfigurationProvider>(provider => sharedApiConfig)
                .AddSingleton<IRequestHandler<GetAllOrdersByStatusQuery, ResponseWrapper<Order>>,
                    GetAllOrdersByStatusHandler>()
                .AddSingleton<IChannelEngineRepositoryWrapper, ChannelEngineRepositoryWrapper>()
                .AddSingleton<IChannelEngineApiClientFactory, ChannelEngineApiClientFactory>()
                .AddSingleton<IChannelEngineApiRequestFactory, ChannelEngineApiRequestFactory>()
                .AddTransient<IOrdersRepository, OrdersRepository>();

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
