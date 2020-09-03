using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Contracts;
using Models;

namespace Services
{
    public class OrdersService : IOrdersService
    {
        private readonly HttpClient _httpClient;

        public OrdersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Order>> GetAllWithStatusAsync(string status)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["statuses"] = status;
            var response = await _httpClient.GetAsync($"orders?{query}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Server returned status code: {(int)response.StatusCode} {response.StatusCode}");
            }

            var resultStream = response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<ResponseWrapper<Order>>(await resultStream);
            
            return result.Content;
        }
    }
}