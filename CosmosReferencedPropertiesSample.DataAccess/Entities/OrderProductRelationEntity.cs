// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.DataAccess.Entities
{
  using System;

  public sealed class OrderProductRelationEntity : EntityBase
  {
    public Guid OrderId { get; set; }

    public OrderEntity Order { get; set; }

    public Guid ProductId { get; set; }

    public ProductEntity Product { get; set; }

    public string Name { get; set; }

    public string Sku { get; set; }
  }
}
