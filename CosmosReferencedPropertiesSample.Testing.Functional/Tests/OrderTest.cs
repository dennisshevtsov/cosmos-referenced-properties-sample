// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.Testing.Functional.Tests
{
  using System;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.VisualStudio.TestTools.UnitTesting;

  using CosmosReferencedPropertiesSample.DataAccess.Defaults;
  using CosmosReferencedPropertiesSample.DataAccess.Entities;
  using CosmosReferencedPropertiesSample.DataAccess.Extensions;
  using CosmosReferencedPropertiesSample.DataAccess.Repositories;
  using CosmosReferencedPropertiesSample.Testing.Functional.Helpers;

  [TestClass]
  public class OrderTest
  {
    private IDisposable _disposable;

    private IProductRepository _productRepository;
    private IOrderRepository _orderRepository;

    [TestInitialize]
    public void Initialize()
    {
      var provider = new ServiceCollection().AddDataAccess("https://localhost:8081/", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", "crpsdb")
                                            .BuildServiceProvider();

      _disposable = provider;
      _productRepository = provider.GetRequiredService<IProductRepository>();
      _orderRepository = provider.GetRequiredService<IOrderRepository>();
    }

    [TestCleanup]
    public void Cleanup() => _disposable?.Dispose();

    [TestMethod]
    public async Task Create_Order_Then_Update_Product_Then_Get_Order_Products_Test()
    {
      var product0 = await _productRepository.CreateProducAsync(
        ProductGenerator.GenerateProduct(), CancellationToken.None);
      var product1 = await _productRepository.CreateProducAsync(
        ProductGenerator.GenerateProduct(), CancellationToken.None);
      var product2 = await _productRepository.CreateProducAsync(
        ProductGenerator.GenerateProduct(), CancellationToken.None);

      var order = new OrderEntity
      {
        CreatedOn = DateTime.UtcNow,
        ProductIdCollection = new[]
        {
          Guid.NewGuid(),
          Guid.NewGuid(),
          product0.Id,
          Guid.NewGuid(),
          product1.Id,
          product2.Id,
          Guid.NewGuid(),
        }
      };

      order = await _orderRepository.CreateOrderAsync(order, CancellationToken.None);

      Assert.IsNotNull(order);
      Assert.IsNotNull(order.Products);
      Assert.IsNotNull(order.ProductIdCollection);

      var products = order.Products.ToArray();

      Assert.AreEqual(3, products.Length);
      Assert.IsTrue(products.Any(product => product.Id == product0.Id));
      Assert.IsTrue(products.Any(product => product.Id == product1.Id));
      Assert.IsTrue(products.Any(product => product.Id == product2.Id));

      var productIdCollection = order.ProductIdCollection.ToArray();

      Assert.AreEqual(3, productIdCollection.Length);
      Assert.IsTrue(productIdCollection.Any(id => id == product0.Id));
      Assert.IsTrue(productIdCollection.Any(id => id == product1.Id));
      Assert.IsTrue(productIdCollection.Any(id => id == product2.Id));

      product0.Sku = ProductGenerator.GenerateToken();

      await _productRepository.UpdateProducAsync(product0, CancellationToken.None);

      var productsForOrder = await _productRepository.GetProductsForOrderAsync(
        order.Id, OrderProductSortProperty.Sku, SortDirection.Descending, CancellationToken.None);

      Assert.IsNotNull(productsForOrder);

      var controlProductsForOrder = new[] { product0, product1, product2, }
                                    .OrderByDescending(product => product.Sku)
                                    .ToArray();
      var testProductsForOrder = productsForOrder.ToArray();

      Assert.AreEqual(controlProductsForOrder.Length, testProductsForOrder.Length);

      for (var i = 0; i < controlProductsForOrder.Length; ++i)
      {
        Assert.AreEqual(controlProductsForOrder[i].Id, testProductsForOrder[i].Id);
        Assert.AreEqual(controlProductsForOrder[i].Name, testProductsForOrder[i].Name);
        Assert.AreEqual(controlProductsForOrder[i].Description, testProductsForOrder[i].Description);
        Assert.AreEqual(controlProductsForOrder[i].Sku, testProductsForOrder[i].Sku);
      }
    }
  }
}
