using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Contracts.ApiClient;
using Contracts.ApiClient.Factories;
using Models;
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

        public async Task<ResponseWrapper<IEnumerable<Product>>> GetProductsByMerchantNo(IEnumerable<string> productIds)
        {
            var request = RequestFactory
                .CreateRequest(SharedConfig.ProductsEndpoint)
                .AddParameter(MerchantProductListParam, string.Join(',', productIds));
            
            var result = await ClientFactory
                .CreateClient()
                .GetAsync<ResponseWrapper<IEnumerable<Product>>>(request);
            
            return result;
        }

        public async Task<ResponseWrapper<Product>> GetProduct(string productId)
        {
            var request = RequestFactory
                .CreateRequest($"{SharedConfig.ProductsEndpoint}/{productId}");

            var result = await ClientFactory
                .CreateClient()
                .GetAsync<ResponseWrapper<Product>>(request);

            return result;
        }
    }
}