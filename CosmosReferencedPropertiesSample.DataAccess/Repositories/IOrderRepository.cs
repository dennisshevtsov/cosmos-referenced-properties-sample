// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.DataAccess.Repositories
{
  using System.Threading;
  using System.Threading.Tasks;

  using CosmosReferencedPropertiesSample.DataAccess.Entities;

  public interface IOrderRepository
  {
    public Task<OrderEntity> CreateOrderAsync(
      OrderEntity orderEntity, CancellationToken cancellationToken);
  }
}
