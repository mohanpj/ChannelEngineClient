﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Contracts.ApiClient;
using Contracts.ApiClient.Factories;
using Microsoft.AspNetCore.JsonPatch;
using Models;
using Repository.API;
using RestSharp;

namespace Repository
{
    public class ProductsRepository : ApiEndpointBase, IProductsRepository
    {
        private const string MerchantProductListParam = "merchantProductNoList";

        public ProductsRepository(IChannelEngineApiClientFactory clientFactory,
            IChannelEngineApiRequestFactory requestFactory,
            ISharedApiConfigurationProvider sharedConfig)
            : base(clientFactory, requestFactory, sharedConfig)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByMerchantNo(IEnumerable<string> productIds)
        {
            var request = RequestFactory
                .CreateRequest(SharedConfig.ProductsEndpoint)
                .AddParameter(MerchantProductListParam, string.Join(',', productIds));

            var result = await ClientFactory
                .CreateClient()
                .ExecuteGetAsync<ResponseWrapper<IEnumerable<Product>>>(request);

            EnsureSuccess(result);

            return result.Data.Content;
        }

        public async Task<Product> GetProduct(string productId)
        {
            var request = RequestFactory
                .CreateRequest($"{SharedConfig.ProductsEndpoint}/{productId}");

            var result = await ClientFactory
                .CreateClient()
                .ExecuteGetAsync<ResponseWrapper<Product>>(request);

            EnsureSuccess(result);

            return result.Data.Content;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var patchRequestBody = new JsonPatchDocument<Product>().Replace(p => p.Stock, product.Stock);

            var request = RequestFactory
                .CreateRequest($"{SharedConfig.ProductsEndpoint}/{product.MerchantProductNo}")
                .AddJsonBody(patchRequestBody);

            var result = await ClientFactory
                .CreateClient()
                .ExecuteAsync<ResponseWrapper<Product>>(request, Method.PATCH);

            EnsureSuccess(result);

            var updatedProduct = await GetProduct(product.MerchantProductNo);

            return updatedProduct;
        }
    }
}