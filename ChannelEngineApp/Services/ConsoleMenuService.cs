using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiClient.Extensions;
using ConsoleTables;
using Contracts.ApiClient;
using Contracts.Console;
using Models;
using Utilities;

namespace ChannelEngineConsoleApp.Services
{
    public class ConsoleMenuService : IConsoleMenuService
    {
        private readonly IChannelEngineApiService _apiService;
        private readonly IConsolePrintingService _printer;

        public ConsoleMenuService(IConsolePrintingService printer, IChannelEngineApiService apiService)
        {
            _printer = printer;
            _apiService = apiService;
        }

        public async Task RunAsync()
        {
            var showMenu = true;
            while (showMenu) showMenu = await MainMenu();
        }

        private async Task<bool> MainMenu()
        {
            _printer.Clear();
            _printer.WriteLine("CHANNEL ENGINE CONSOLE\n");
            _printer.WriteLine("Choose option:");
            _printer.WriteLine("1) Get all orders with status");
            _printer.WriteLine("0) EXIT");
            _printer.WriteLine();
            var key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.D1:
                    await ShowAllOrdersWithStatus();
                    return true;
                case ConsoleKey.D0:
                    return false;
                default:
                    _printer.WriteLine();
                    _printer.WriteLine("Invalid option selected. Try again.");
                    Console.ReadKey();
                    return true;
            }
        }

        private async Task ShowAllOrdersWithStatus()
        {
            var status = SelectOrderStatus();

            _printer.Clear();
            _printer.WriteLine("CHANNEL ENGINE CONSOLE\n");

            try
            {
                var result = await _apiService.GetOrdersWithStatus(status);
                var response = result.ToArray();

                if (response.Any())
                {
                    var table = new ConsoleTable("ID", "Channel name", "No of products");
                    foreach (var order in response)
                        table.AddRow(order.Id, order.ChannelName, order.Lines.Sum(l => l.Quantity));

                    _printer.WriteTable(table, Format.Minimal);
                }
                else
                {
                    _printer.WriteLine("No data to display.");
                    _printer.WriteLine();
                    _printer.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    return;
                }

                while (true)
                {
                    _printer.WriteLine("1) Get top 5 products sold");
                    _printer.WriteLine("0) Return to main menu");
                    _printer.WriteLine();
                    var key = Console.ReadKey().Key;

                    switch (key)
                    {
                        case ConsoleKey.D1:
                            await ShowTopFiveProducts(response);
                            break;
                        case ConsoleKey.D0:
                            break;
                        default:
                            Console.WriteLine("\nInvalid option selected. Try again...");
                            continue;
                    }

                    break;
                }
            }
            catch (ChannelEngineApiClientException apiClientException)
            {
                HandleException(apiClientException);
            }
        }

        private OrderStatus SelectOrderStatus()
        {
            _printer.Clear();
            _printer.WriteLine("CHANNEL ENGINE CONSOLE\n");
            _printer.WriteLine("Available statuses");

            var orderStatusNames = Enum.GetNames(typeof(OrderStatus));

            for (var i = 1; i <= orderStatusNames.Length; i++)
                _printer.WriteLine($"{i - 1}) {orderStatusNames[i - 1]}");

            _printer.WriteLine();
            _printer.WriteLine("Select status:");

            var key = Console.ReadLine();

            while (!InputIsValid(key))
            {
                _printer.WriteLine("Invalid status selected. Try again");
                key = Console.ReadLine();
            }

            return Enum.Parse<OrderStatus>(key!);
        }

        private static bool InputIsValid(string key)
        {
            return key != string.Empty &&
                   int.TryParse(key, out var keyValue) &&
                   Enum.IsDefined(typeof(OrderStatus), keyValue);
        }

        private async Task ShowTopFiveProducts(IEnumerable<Order> orders)
        {
            var products = await _apiService.GetTopSoldProductsFromOrders(orders);
            var topProducts = products.ToArray();

            _printer.Clear();
            _printer.WriteLine("CHANNEL ENGINE CONSOLE\n");

            var table = new ConsoleTable("ID", "Name", "Ean", "Quantity");
            foreach (var product in topProducts)
                table.AddRow(product.MerchantProductNo, product.Name, product.Ean, product.TotalSold);

            table.Write(Format.Minimal);

            while (true)
            {
                _printer.WriteLine("1) Update product stock to 25");
                _printer.WriteLine("0) Return to main menu");
                _printer.WriteLine();
                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        await SelectProductToUpdate(topProducts.ToArray());
                        break;
                    case ConsoleKey.D0:
                        break;
                    default:
                        _printer.WriteLine("\nInvalid option selected. Try again...");
                        continue;
                }

                break;
            }
        }

        private async Task SelectProductToUpdate(IReadOnlyList<TopProductDto> products)
        {
            _printer.Clear();
            _printer.WriteLine("CHANNEL ENGINE CONSOLE\n");
            _printer.WriteLine("Choose product:");
            for (var i = 0; i < products.Count; i++)
                _printer.WriteLine($"{i + 1}) {products[i].MerchantProductNo} {products[i].Name}");

            _printer.WriteLine("0) Return to main menu");
            _printer.WriteLine();
            var key = Console.ReadKey();

            while (!IsValidInputOption(key, products.Count))
            {
                _printer.WriteLine("\nInvalid option selected. Try again...");
                key = Console.ReadKey();
            }

            if (key.Key == ConsoleKey.D0) return;

            var keyValue = ConsoleKeyUtilities.GetIntFromDigitKey(key);

            await UpdateProductStock(products[keyValue - 1]);
        }

        private async Task UpdateProductStock(TopProductDto productDto)
        {
            var updatedProduct = await _apiService.UpdateProductStock(productDto);

            _printer.Clear();
            _printer.WriteLine("CHANNEL ENGINE CONSOLE\n");
            _printer.WriteLine(
                $"Merchant product Np {updatedProduct.MerchantProductNo} stock has been updated to {updatedProduct.Stock}.");
            _printer.WriteLine();
            _printer.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }

        private void HandleException(ChannelEngineApiClientException ex)
        {
            _printer.WriteLine("Something went wrong.");
            _printer.WriteLine($"Status Code: {ex.StatusCode}");
            _printer.WriteLine($"Message:\n{ex.Message}");
            _printer.WriteLine();
            _printer.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private bool IsValidInputOption(ConsoleKeyInfo key, int optionsCount)
        {
            if (!char.IsDigit(key.KeyChar)) return false;

            var value = ConsoleKeyUtilities.GetIntFromDigitKey(key);

            return value >= 0 && value <= optionsCount;
        }
    }
}