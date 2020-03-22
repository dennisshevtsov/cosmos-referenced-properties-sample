// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.DataAccess.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  using CosmosReferencedPropertiesSample.DataAccess.Entities;

  internal sealed class OrderProductRelationEntityTypeConfiguration : EntityTypeConfigurationBase<OrderProductRelationEntity>
  {
    protected override void ConfigureInternal(EntityTypeBuilder<OrderProductRelationEntity> builder)
    {
      builder.Property(entity => entity.OrderId).ToJsonProperty("orderId").IsRequired();
      builder.Property(entity => entity.ProductId).ToJsonProperty("productId").IsRequired();

      builder.Property(entity => entity.Name).ToJsonProperty("name").IsRequired().HasMaxLength(256);
      builder.Property(entity => entity.Description).ToJsonProperty("description").IsRequired().HasMaxLength(512);
      builder.Property(entity => entity.Sku).ToJsonProperty("sku").IsRequired().HasMaxLength(16);

      builder.HasOne(entity => entity.Order)
             .WithMany(entity => entity.OrderProductRelations)
             .HasForeignKey(entity => entity.OrderId)
             .HasPrincipalKey(entity => entity.Id);
      builder.HasOne(entity => entity.Product)
             .WithMany(entity => entity.OrderProductRelations)
             .HasForeignKey(entity => entity.ProductId)
             .HasPrincipalKey(entity => entity.Id);
    }
  }
}
