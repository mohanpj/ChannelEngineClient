using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleTables;
using Contracts.Console;
using MediatR;
using Models;
using Repository.API.Commands;

namespace ChannelEngineConsoleApp.Services
{
    public class ConsoleMenuService : IConsoleMenuService
    {
        private readonly IMediator _mediator;
        private readonly IConsolePrintingService _printer;

        public ConsoleMenuService(IMediator mediator, IConsolePrintingService printer)
        {
            _mediator = mediator;
            _printer = printer;
        }

        public async Task RunAsync()
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = await MainMenu();
            }
        }

        private async Task<bool> MainMenu()
        {
            _printer.Clear();
            _printer.WriteLine("CHANNEL ENGINE CONSOLE\n");
            _printer.WriteLine("Choose option:");
            _printer.WriteLine("1) Get all orders with status");
            _printer.WriteLine("2) Set product quantity to 25");
            _printer.WriteLine("0) EXIT");
            _printer.WriteLine();
            var key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.D1:
                    await ShowAllOrdersWithStatus();
                    return true;
                case ConsoleKey.D2:
                    SelectProductToUpdate();
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

            var query = new GetAllOrdersByStatusQuery(status);
            var response = await _mediator.Send(query);

            if (response.Success)
            {
                if (response.Content.Any())
                {
                    var table = new ConsoleTable("ID", "Channel name", "No of products");
                    foreach (var order in response.Content)
                    {
                        table.AddRow(order.Id, order.ChannelName, order.Lines.Sum(l => l.Quantity));
                    }
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
                    _printer.WriteLine("0) BACK");
                    _printer.WriteLine();
                    var key = Console.ReadKey().Key;
                    
                    switch (key)
                    {
                        case ConsoleKey.D1:
                            ShowTopFiveProducts(response.Content);
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
            else
            {
                HandleError(response);
            }
        }

        private OrderStatus SelectOrderStatus()
        {
            _printer.Clear();
            _printer.WriteLine("CHANNEL ENGINE CONSOLE\n");
            _printer.WriteLine("Available statuses");
            
            var orderStatusNames = Enum.GetNames(typeof(OrderStatus));
            
            for (var i = 1; i <= orderStatusNames.Length; i++)
            {
                _printer.WriteLine($"{i - 1}) {orderStatusNames[i - 1]}");
            }
            
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
                   int.TryParse(key, out int keyValue) &&
                   Enum.IsDefined(typeof(OrderStatus), keyValue);
        }

        private void ShowTopFiveProducts(IEnumerable<Order> orders)
        {
            var products = orders.SelectMany(o => o.Lines)
                .OrderByDescending(p => p.Quantity)
                .Take(5)
                .ToArray();

            _printer.Clear();
            _printer.WriteLine("CHANNEL ENGINE CONSOLE\n");

            var table = new ConsoleTable("ID", "Name", "Ean", "Quantity");
            foreach (var product in products)
            {
                table.AddRow(product.MerchantProductNo, product.Description, product.Gtin, product.Quantity);
            }

            table.Write(Format.Minimal);

            _printer.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }

        private void SelectProductToUpdate()
        {
            while (true)
            {
                _printer.Clear();
                _printer.WriteLine("CHANNEL ENGINE CONSOLE\n");
                _printer.WriteLine("Choose product:");
                _printer.WriteLine("1) Dummy product1");
                _printer.WriteLine("2) Dummy product2");
                _printer.WriteLine("3) Dummy product3");
                _printer.WriteLine("4) Dummy product4");
                _printer.WriteLine("5) Dummy product5");
                _printer.WriteLine("0) BACK");
                _printer.WriteLine();

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
                        _printer.WriteLine();
                        _printer.WriteLine("Invalid option selected. Press any key to try again.");
                        Console.ReadKey();
                        continue;
                }

                break;
            }
        }

        private void UpdateProductStock(string productName)
        {
            _printer.Clear();
            _printer.WriteLine("CHANNEL ENGINE CONSOLE\n");
            _printer.WriteLine($"{productName} stock has been updated.");
            _printer.WriteLine();
            _printer.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }

        private void HandleError(BaseResponseWrapper response)
        {
            _printer.WriteLine("Something went wrong.");
            _printer.WriteLine($"Status Code: {response.StatusCode}");
            _printer.WriteLine($"Message:\n{response.Message}");
            _printer.WriteLine();
            _printer.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}