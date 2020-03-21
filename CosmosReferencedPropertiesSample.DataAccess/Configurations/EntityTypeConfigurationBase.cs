// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.DataAccess.Configurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;
  using Microsoft.EntityFrameworkCore.ValueGeneration;

  using CosmosReferencedPropertiesSample.DataAccess.Entities;
  using CosmosReferencedPropertiesSample.DataAccess.ValueGeneration;

  internal abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase
  {
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
      builder.ToContainer("crpscontainer");
      builder.HasDiscriminator("partitionKey", typeof(string));

      builder.HasKey(entity => entity.Id);
      builder.HasPartitionKey("partitionKey");

      builder.Property(entity => entity.Id).ToJsonProperty("primaryKey").HasValueGenerator<GuidValueGenerator>();
      builder.Property(typeof(string), "partitionKey").ToJsonProperty("partitionKey").HasValueGenerator<PartitionKeyValueGenerator>();

      ConfigureInternal(builder);
    }

    protected abstract void ConfigureInternal(EntityTypeBuilder<TEntity> builder);
  }
}
