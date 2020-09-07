using System.Collections.Generic;
using System.IO;
using ApiClient.Commands;
using ApiClient.Factories;
using ApiClient.Handlers;
using ApiClient.Queries;
using ApiClient.Services;
using Contracts.ApiClient;
using Contracts.ApiClient.Factories;
using Contracts.Repository;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Repository;

namespace Shared
{
    public static class SharedConfigurationExtension
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration config)
        {
            var settings = new SharedSettingsProvider();
            services.AddMediatR(typeof(GetAllOrdersByStatusHandler).Assembly);
            config.GetSection(SharedSettingsProvider.SettingsRoot).Bind(settings);
            services.AddSingleton<ISharedSettingsProvider, SharedSettingsProvider>(provider => settings)
                .AddRepositories()
                .AddFactories()
                .AddRequestHandlers()
                .AddSingleton<IChannelEngineApiService, ChannelEngineApiService>();

            return services;
        }

        public static IConfigurationBuilder AddSharedConfiguration(this IConfigurationBuilder config,
            IHostEnvironment env)
        {
            var sharedDirectory = Path.Combine(env.ContentRootPath, "..", "Shared");

            config.AddJsonFile(Path.Combine(sharedDirectory, "sharedSettings.json"), true, true)
                .AddJsonFile(Path.Combine(sharedDirectory, $"sharedSettings.{env.EnvironmentName}.json"), true, true);

            if (env.IsDevelopment())
                config.AddUserSecrets(typeof(SharedSettingsProvider).Assembly);

            return config;
        }

        private static IServiceCollection AddRequestHandlers(this IServiceCollection services) =>
            services.AddSingleton<IRequestHandler<GetAllOrdersByStatusQuery, IEnumerable<Order>>,
                    GetAllOrdersByStatusHandler>()
                .AddSingleton<IRequestHandler<GetTopSoldProductsFromOrdersQuery, IEnumerable<TopProductDto>>,
                    GetTopSoldProductsHandler>()
                .AddSingleton<IRequestHandler<UpdateProductStockCommand, Product>, UpdateProductStockHandler>()
                .AddSingleton<IRequestHandler<GetProductsByIdsQuery, IEnumerable<Product>>, GetProductsByIdsHandler>();

        private static IServiceCollection AddFactories(this IServiceCollection services) =>
            services.AddSingleton<IChannelEngineApiClientFactory, ChannelEngineApiClientFactory>()
                .AddSingleton<IChannelEngineApiRequestFactory, ChannelEngineApiRequestFactory>();

        private static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services.AddTransient<IOrdersRepository, OrdersRepository>()
                .AddTransient<IProductsRepository, ProductsRepository>()
                .AddSingleton<IChannelEngineRepositoryWrapper, ChannelEngineRepositoryWrapper>();
    }
}