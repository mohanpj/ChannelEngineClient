using System;
using System.Net.Http;
using System.Threading.Tasks;

using Contracts;

namespace Services
{
    public class ConsoleMenuService : IConsoleMenuService
    {
        private readonly IChannelEngineServiceWrapper _channelEngineService;

        public ConsoleMenuService(IChannelEngineServiceWrapper channelEngineService)
        {
            _channelEngineService = channelEngineService;
        }

        public async Task DrawMenuAsync()
        {
            bool showMenu = true;

            while (showMenu)
            {
                showMenu = await MainMenu();
            }
        }

        private async Task<bool> MainMenu()
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
                    await ShowAllProductsWithStatus();
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
                    Console.WriteLine();
                    Console.WriteLine("Invalid option selected. Press any key to try again.");
                    Console.ReadKey();
                    return true;
            }
        }

        private async Task ShowAllProductsWithStatus()
        {
            Console.Clear();
            Console.WriteLine("CHANNEL ENGINE CONSOLE\n");

            try
            {
                var orders = await _channelEngineService.Orders.GetAllWithStatusAsync("IN_PROGRESS");
                foreach (var order in orders)
                {
                    Console.WriteLine($"{order.Id} {order.ChannelName} {order.GlobalChannelName}");
                }
            }
            catch (HttpRequestException ex)
            {
                HandleError(ex.Message);
            }
            
            Console.WriteLine();
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }

        private void ShowTopFiveProducts()
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

        private void SelectProductToUpdate()
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

        private void UpdateProduct(string productName)
        {
            Console.Clear();
            Console.WriteLine("CHANNEL ENGINE CONSOLE\n");
            Console.WriteLine($"{productName} quantity has been updated.");
            Console.WriteLine();
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }

        private void HandleError(string message)
        {
            Console.WriteLine("Something went wrong.");
            Console.WriteLine($"Message:\n{message}");
        }
    }
}