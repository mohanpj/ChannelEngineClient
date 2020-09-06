using System.Collections.Generic;
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
using Repository.API.Queries;

namespace Shared
{
    public static class SharedConfigurationExtension
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration config)
        {
            var sharedApiConfig = new SharedApiConfigurationProvider();
            config.GetSection(SharedApiConfigurationProvider.SettingsRoot).Bind(sharedApiConfig);
            services.AddMediatR(typeof(GetAllOrdersByStatusHandler).Assembly)
                .AddSingleton<ISharedApiConfigurationProvider, SharedApiConfigurationProvider>(provider =>
                    sharedApiConfig)
                .AddRepositories()
                .AddFactories()
                .AddRequestHandlers();


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

        private static IServiceCollection AddRequestHandlers(this IServiceCollection services) =>
            services.AddSingleton<IRequestHandler<GetAllOrdersByStatusQuery, IEnumerable<Order>>,
                    GetAllOrdersByStatusHandler>()
                .AddSingleton<IRequestHandler<GetTopSoldProductsFromOrdersQuery, IEnumerable<TopProductDto>>,
                    GetTopSoldProductsHandler>()
                .AddSingleton<IRequestHandler<UpdateProductStockCommand, Product>, UpdateProductStockHandler>();

        private static IServiceCollection AddFactories(this IServiceCollection services) =>
            services.AddSingleton<IChannelEngineApiClientFactory, ChannelEngineApiClientFactory>()
                .AddSingleton<IChannelEngineApiRequestFactory, ChannelEngineApiRequestFactory>();

        private static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services.AddTransient<IOrdersRepository, OrdersRepository>()
                .AddTransient<IProductsRepository, ProductsRepository>()
                .AddSingleton<IChannelEngineRepositoryWrapper, ChannelEngineRepositoryWrapper>();
    }
}