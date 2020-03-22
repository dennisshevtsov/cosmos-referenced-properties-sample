// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.DataAccess.Repositories
{
  using System;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using AutoMapper;

  using CosmosReferencedPropertiesSample.DataAccess.Entities;

  internal sealed class OrderRepository : IOrderRepository
  {
    private readonly IMapper _mapper;
    private readonly IDbContextProvider _dbContextProvider;
    private readonly IProductRepository _productRepository;

    public OrderRepository(IMapper mapper,
                           IDbContextProvider dbContextProvider,
                           IProductRepository productRepository)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

        foreach (var productEntity in orderEntity.Products)
        {
          var relation = new OrderProductRelationEntity();

          _mapper.Map(orderEntity, relation);
          _mapper.Map(productEntity, relation);

          context.Add(relation);
        }

        await context.SaveChangesAsync(cancellationToken);

        return orderEntity;
      }
    }
  }
}
