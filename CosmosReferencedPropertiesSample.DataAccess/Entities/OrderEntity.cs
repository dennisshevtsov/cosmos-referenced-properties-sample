// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.DataAccess.Entities
{
  using System;
  using System.Collections.Generic;

  public sealed class OrderEntity : EntityBase
  {
    public DateTime CreatedOn { get; set; }

    public DateTime? OpenedOn { get; set; }

    public DateTime? CompletedOn { get; set; }

    public bool IsNew => OpenedOn == null && CompletedOn == null;

    public bool IsOpen => OpenedOn != null && CompletedOn == null;

    public bool IsCompleted => OpenedOn != null && CompletedOn != null;

    public IEnumerable<OrderProductRelationEntity> OrderProductRelations { get; set; }

    public IEnumerable<ProductEntity> Products { get; set; }

    public IEnumerable<Guid> ProductIdCollection { get; set; }
  }
}
