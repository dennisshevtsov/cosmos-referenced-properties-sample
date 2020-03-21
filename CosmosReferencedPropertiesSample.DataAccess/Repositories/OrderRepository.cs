// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.DataAccess.Repositories
{
  using System;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using CosmosReferencedPropertiesSample.DataAccess.Entities;

  internal sealed class OrderRepository : IOrderRepository
  {
    private readonly IDbContextProvider _dbContextProvider;
    private readonly IProductRepository _productRepository;

    public OrderRepository(IDbContextProvider dbContextProvider, IProductRepository productRepository)
    {
      _dbContextProvider = dbContextProvider ?? throw new ArgumentNullException(nameof(dbContextProvider));
      _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<OrderEntity> CreateOrderAsync(
      OrderEntity orderEntity, CancellationToken cancellationToken)
    {
      using (var context = _dbContextProvider.GetNewDbContext())
      {
        context.Add(orderEntity);

        await context.SaveChangesAsync(cancellationToken);

        orderEntity.Products = await _productRepository.GetProductsAsync(
          orderEntity.ProductIdCollection, cancellationToken);
        orderEntity.ProductIdCollection = orderEntity.Products.Select(entity => entity.Id);

        foreach (var product in orderEntity.Products)
        {
          context.Add(new OrderProductRelationEntity
          {
            OrderId = orderEntity.Id,
            ProductId = product.Id,
            Sku = product.Sku,
            Name = product.Name,
          });
        }

        await context.SaveChangesAsync(cancellationToken);

        return orderEntity;
      }
    }
  }
}
