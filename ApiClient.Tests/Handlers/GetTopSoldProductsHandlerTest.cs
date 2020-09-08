using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiClient.Handlers;
using ApiClient.Queries;
using Contracts.Repository;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Models;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ApiClient.Tests.Handlers
{
    public class GetTopSoldProductsHandlerTest
    {
        private Mock<IChannelEngineRepositoryWrapper> _repositoryMock;
        private const int TopFive = 5;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IChannelEngineRepositoryWrapper>();
            _repositoryMock.Setup(r => r.Orders
                    .GetAllOrdersWithStatus(It.IsAny<OrderStatus>()))
                .Returns((string status) => Task.FromResult(OrdersMockData.Where(o => o.Status == status)));
            _repositoryMock.Setup(r => r.Products
                    .GetProductsByMerchantNo(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> merchantProductIds) =>
                {
                    var products = merchantProductIds
                        .Select(prodNo => ProductsMockData.FirstOrDefault(md => md.MerchantProductNo == prodNo))
                        .Where(res => res != null);
                    return Task.FromResult<IEnumerable<Product>>(products);
                });
        }

        [TestCaseSource(nameof(TestCases))]
        public async Task QueryHandler_ReturnsDescendingOrderedTopProductsSold(
            GetTopSoldProductsFromOrdersQuery query,
            IEnumerable<TopProductDto> expectedProducts)
        {
            var queryHandler = new GetTopSoldProductsHandler(_repositoryMock.Object);
            var result = await queryHandler.Handle(query, new CancellationToken());
            result.Should().BeEquivalentTo(expectedProducts, options => options.WithStrictOrdering());
        }

        private static IEnumerable TestCases
        {
            get
            {
                var allPossibleProducts = new[]
                {
                    CreateTopProduct("3", 17),
                    CreateTopProduct("2", 13),
                    CreateTopProduct("1", 6),
                    CreateTopProduct("6", 5),
                    CreateTopProduct("4", 5),
                    CreateTopProduct("5", 3)
                };
                var inProgressProducts = new[]
                {
                    CreateTopProduct("3", 6),
                    CreateTopProduct("1", 3),
                    CreateTopProduct("4", 2),
                    CreateTopProduct("2", 1)
                };
                var newProducts = new[]
                {
                    CreateTopProduct("3", 6),
                    CreateTopProduct("2", 6),
                    CreateTopProduct("4", 2),
                    CreateTopProduct("1", 2),
                    CreateTopProduct("5", 1),
                };
                yield return new TestCaseData(
                    CreateQuery(OrdersMockData.Where(o => o.Status == GetStatusName(OrderStatus.IN_PROGRESS))),
                        inProgressProducts)
                    .SetName($"order status: IN_PROGRESS");
                yield return new TestCaseData(CreateQuery(OrdersMockData), allPossibleProducts.Take(TopFive))
                    .SetName($"order status: NONE");
                yield return new TestCaseData(CreateQuery(OrdersMockData.Where(o => o.Status == GetStatusName(OrderStatus.NEW))),
                        newProducts).SetName($"order status: NEW");
                yield return new TestCaseData(CreateQuery(new List<Order>()), new List<TopProductDto>())
                    .SetName("no orders available");
            }
        }

        private static TopProductDto CreateTopProduct(string merchantProductNo, int totalQuantity) =>
            new TopProductDto
            {
                MerchantProductNo = merchantProductNo,
                TotalSold = totalQuantity,
                Name = $"test|{merchantProductNo}|name",
                Ean = $"test|{merchantProductNo}|ean",
            };

        private static readonly IEnumerable<Order> OrdersMockData = new[]
        {
            PrepareMockOrder(OrderStatus.NEW, ("1", 1), ("2", 3), ("3", 5)),
            PrepareMockOrder(OrderStatus.NEW, ("4", 2), ("2", 1), ("1", 1)),
            PrepareMockOrder(OrderStatus.NEW, ("5", 1), ("2", 2), ("3", 1)),
            PrepareMockOrder(OrderStatus.IN_PROGRESS, ("1", 2), ("3", 2)),
            PrepareMockOrder(OrderStatus.IN_PROGRESS, ("4", 1), ("3", 1)),
            PrepareMockOrder(OrderStatus.IN_PROGRESS, ("1", 1)),
            PrepareMockOrder(OrderStatus.IN_PROGRESS, ("4", 1), ("3", 3), ("2", 1)),
            PrepareMockOrder(OrderStatus.CLOSED, ("1", 1), ("2", 1), ("3", 1)),
            PrepareMockOrder(OrderStatus.CLOSED, ("5", 2), ("2", 2), ("3", 3), ("4", 1)),
            PrepareMockOrder(OrderStatus.CLOSED, ("6", 5), ("2", 3), ("3", 1)),
            PrepareMockOrder(OrderStatus.REQUIRES_CORRECTION),
        };

        private static readonly IEnumerable<Product> ProductsMockData = new[]
        {
            PrepareMockProduct("1"),
            PrepareMockProduct("2"),
            PrepareMockProduct("3"),
            PrepareMockProduct("4"),
            PrepareMockProduct("5"),
            PrepareMockProduct("6"),
            PrepareMockProduct("7"),
        };

        private static GetTopSoldProductsFromOrdersQuery CreateQuery(IEnumerable<Order> orders, int count = TopFive) =>
            new GetTopSoldProductsFromOrdersQuery(orders, count);

        private static Order PrepareMockOrder(OrderStatus status,
            params (string merchantProductNo, int quantity)[] lines) =>
            new Order
            {
                Status = GetStatusName(status),
                Lines = lines.Select(l => new ProductLine()
                {
                    MerchantProductNo = l.merchantProductNo,
                    Quantity = l.quantity
                }).ToList()
            };

        private static Product PrepareMockProduct(string merchantProductNo) =>
            new Product
            {
                MerchantProductNo = merchantProductNo,
                Name = $"test|{merchantProductNo}|name",
                Ean = $"test|{merchantProductNo}|ean"
            };

        private static string GetStatusName(OrderStatus status) => Enum.GetName(typeof(OrderStatus), status);
    }
}