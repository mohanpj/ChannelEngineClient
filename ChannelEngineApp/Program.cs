using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ChannelEngineApp
{
    static class Program
    {
        private static IConfiguration _config;
        private static readonly HttpClient _httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            CreateConfiguration();
            ConfigureClient();

            await StartApp();
        }
        private static void CreateConfiguration()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }

        private static void ConfigureClient()
        {
            _httpClient.BaseAddress = new Uri($"{_config["AppConfig:ApiBaseUri"]}{_config["AppConfig:ApiVersion"]}");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", _config["AppConfig:UserAgent"]);
        }

        private static async Task StartApp()
        {
            Console.WriteLine("HelloWorld");
        }
    }
}
