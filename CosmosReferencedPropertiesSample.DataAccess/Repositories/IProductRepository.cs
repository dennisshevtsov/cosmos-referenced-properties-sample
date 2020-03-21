// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.DataAccess.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using CosmosReferencedPropertiesSample.DataAccess.Defaults;
  using CosmosReferencedPropertiesSample.DataAccess.Entities;

  public interface IProductRepository
  {
    public Task<ProductEntity> CreateProducAsync(
      ProductEntity productEntity, CancellationToken cancellationToken);

    public Task<ProductEntity> UpdateProducAsync(
      ProductEntity productEntity, CancellationToken cancellationToken);

    public Task<IEnumerable<ProductEntity>> GetProductsForOrderAsync(
      Guid orderId,
      OrderProductSortProperty sortProperty,
      SortDirection sortDirection,
      CancellationToken cancellationToken);

    public Task<IEnumerable<ProductEntity>> GetProductsAsync(
      IEnumerable<Guid> productIdCollection, CancellationToken cancellationToken);
  }
}
