// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.DataAccess.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using CosmosReferencedPropertiesSample.DataAccess.Entities;

  internal sealed class OrderEntityTypeConfiguration : EntityTypeConfigurationBase<OrderEntity>
  {
    protected override void ConfigureInternal(EntityTypeBuilder<OrderEntity> builder)
    {
      builder.Property(entity => entity.CreatedOn).ToJsonProperty("createdOn").IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.OpenedOn).ToJsonProperty("openedOn");
      builder.Property(entity => entity.CompletedOn).ToJsonProperty("completedOn");

      builder.HasMany(entity => entity.OrderProductRelations)
             .WithOne(entity => entity.Order)
             .HasForeignKey(entity => entity.OrderId)
             .HasPrincipalKey(entity => entity.Id);

      builder.Ignore(entity => entity.IsNew);
      builder.Ignore(entity => entity.IsOpen);
      builder.Ignore(entity => entity.IsCompleted);
      builder.Ignore(entity => entity.Products);
      builder.Ignore(entity => entity.ProductIdCollection);
    }
  }
}
