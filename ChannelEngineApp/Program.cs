using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace ChannelEngineConsoleApp
{
    static class Program
    {
        private static IConfiguration _config;
        private static readonly HttpClient HttpClient = new HttpClient();

        static void Main(string[] args)
        {
            CreateConfiguration();
            ConfigureClient();

            StartApp();
        }
        private static void CreateConfiguration()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        private static void ConfigureClient()
        {
            HttpClient.BaseAddress = new Uri($"{_config["AppConfig:ApiBaseUri"]}{_config["AppConfig:ApiVersion"]}");
            HttpClient.DefaultRequestHeaders.Add("User-Agent", _config["AppConfig:UserAgent"]);
        }

        private static void StartApp()
        {
            bool showMenu = true;

            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("CHANNEL ENGINE CONSOLE\n");
            Console.WriteLine("Choose option:");
            Console.WriteLine("1) Get all orders with status 'IN_PROGRESS'");
            Console.WriteLine("2) Get top 5 products sold");
            Console.WriteLine("3) Set product quantity to 25");
            Console.WriteLine("0) EXIT");
            Console.WriteLine();
            var key = Console.ReadKey().Key;
            
            switch (key)
            {
                case ConsoleKey.D1:
                    ShowAllProductsWithStatus();
                    return true;
                case ConsoleKey.D2:
                    ShowTopFiveProducts();
                    return true;
                case ConsoleKey.D3:
                    SelectProductToUpdate();
                    return true;
                case ConsoleKey.D0:
                    return false;
                default:
                    return true;
            }
        }

        private static void ShowAllProductsWithStatus()
        {
            Console.Clear();
            Console.WriteLine("CHANNEL ENGINE CONSOLE\n");
            Console.WriteLine("1. | Dummy order");
            Console.WriteLine("2. | Dummy order");
            Console.WriteLine("3. | Dummy order");
            Console.WriteLine("4. | Dummy order");
            Console.WriteLine("5. | Dummy order");
            Console.WriteLine("6. | Dummy order");
            Console.WriteLine("7. | Dummy order");
            Console.WriteLine("8. | Dummy order");
            Console.WriteLine("9. | Dummy order");
            Console.WriteLine("10.| Dummy order");
            Console.WriteLine();
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }

        private static void ShowTopFiveProducts()
        {
            Console.Clear();
            Console.WriteLine("CHANNEL ENGINE CONSOLE\n");
            Console.WriteLine("Choose product:");
            Console.WriteLine("1. | Dummy product  | Quantity: 25");
            Console.WriteLine("2. | Dummy product  | Quantity: 16");
            Console.WriteLine("3. | Dummy product  | Quantity: 15");
            Console.WriteLine("4. | Dummy product  | Quantity: 10");
            Console.WriteLine("5. | Dummy product  | Quantity: 9");
            Console.WriteLine();
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }

        private static void SelectProductToUpdate()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("CHANNEL ENGINE CONSOLE\n");
                Console.WriteLine("Choose product:");
                Console.WriteLine("1) Dummy product1");
                Console.WriteLine("2) Dummy product2");
                Console.WriteLine("3) Dummy product3");
                Console.WriteLine("4) Dummy product4");
                Console.WriteLine("5) Dummy product5");
                Console.WriteLine("0) BACK");
                Console.WriteLine();

                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        UpdateProduct("Dummy product1");
                        break;
                    case ConsoleKey.D2:
                        UpdateProduct("Dummy product2");
                        break;
                    case ConsoleKey.D3:
                        UpdateProduct("Dummy product3");
                        break;
                    case ConsoleKey.D4:
                        UpdateProduct("Dummy product4");
                        break;
                    case ConsoleKey.D5:
                        UpdateProduct("Dummy product5");
                        break;
                    case ConsoleKey.D0:
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid option selected. Press any key to try again.");
                        Console.ReadKey();
                        continue;
                }

                break;
            }
        }

        private static void UpdateProduct(string productName)
        {
            Console.Clear();
            Console.WriteLine("CHANNEL ENGINE CONSOLE\n");
            Console.WriteLine($"{productName} quantity has been updated.");
            Console.WriteLine();
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }
    }
}
