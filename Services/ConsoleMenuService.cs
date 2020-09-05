using System;
using System.Linq;
using System.Threading.Tasks;

using Contracts;
using Models;

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
            Console.WriteLine("1) Get all orders with status");
            Console.WriteLine("2) Get top 5 products sold");
            Console.WriteLine("3) Set product quantity to 25");
            Console.WriteLine("0) EXIT");
            Console.WriteLine();
            var key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.D1:
                    await ShowAllOrdersWithStatus();
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
                    Console.WriteLine("Invalid option selected. Try again.");
                    Console.ReadKey();
                    return true;
            }
        }

        private async Task ShowAllOrdersWithStatus()
        {
            var status = SelectOrderStatus();
            
            Console.Clear();
            Console.WriteLine("CHANNEL ENGINE CONSOLE\n");

            var response = await _channelEngineService.Orders.GetAllWithStatusAsync(status);

            if (response.Success)
            {
                if (response.Content.Any())
                {
                    foreach (var order in response.Content)
                    {
                        Console.WriteLine($"{order.Id, -5}|{order.ChannelName, -20}|{order.Products.Sum(p => p.Quantity), -10}");
                    }
                }
                else
                {
                    Console.WriteLine("No data to display.");
                }

            }
            else
            {
                HandleError(response.Message);
            }
            

            Console.WriteLine();
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }

        private OrderStatus SelectOrderStatus()
        {
            Console.Clear();
            Console.WriteLine("CHANNEL ENGINE CONSOLE\n");

            var orderStatusNames = Enum.GetNames(typeof(OrderStatus));
            for (int i = 1; i <= orderStatusNames.Length; i++)
            {
                Console.WriteLine($"{i-1}) {orderStatusNames[i-1]}");
            }
            Console.WriteLine();
            Console.WriteLine("Select status:");
            var key = Console.ReadLine();
            OrderStatus result;
            
            while (!Enum.TryParse(key, out result))
            {
                Console.WriteLine("Invalid status selected. Try again");
                key = Console.ReadLine();
            }

            return result;
        }

        private void ShowTopFiveProducts()
        {
            Console.Clear();
            Console.WriteLine("CHANNEL ENGINE CONSOLE\n");
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
                        UpdateProductStock("Dummy product1");
                        break;
                    case ConsoleKey.D2:
                        UpdateProductStock("Dummy product2");
                        break;
                    case ConsoleKey.D3:
                        UpdateProductStock("Dummy product3");
                        break;
                    case ConsoleKey.D4:
                        UpdateProductStock("Dummy product4");
                        break;
                    case ConsoleKey.D5:
                        UpdateProductStock("Dummy product5");
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

        private void UpdateProductStock(string productName)
        {
            Console.Clear();
            Console.WriteLine("CHANNEL ENGINE CONSOLE\n");
            Console.WriteLine($"{productName} stock has been updated.");
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