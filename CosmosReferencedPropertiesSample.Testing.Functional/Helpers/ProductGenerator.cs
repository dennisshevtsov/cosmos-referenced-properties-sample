// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.Testing.Functional.Helpers
{
  using System;

  using CosmosReferencedPropertiesSample.DataAccess.Entities;

  public static class ProductGenerator
  {
    public static ProductEntity GenerateProduct() => new ProductEntity
    {
      Name = ProductGenerator.GenerateToken(),
      Description = ProductGenerator.GenerateToken(),
      Sku = ProductGenerator.GenerateToken(),
      Enabled = true,
    };

    public static string GenerateToken() => Guid.NewGuid()
                                                .ToString()
                                                .ToUpper()
                                                .Replace("-", "")
                                                .Substring(0, 5);
  }
}
