// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.DataAccess
{
  using Microsoft.EntityFrameworkCore;

  internal sealed class UnitOfWork : DbContext
  {
    public UnitOfWork(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
      => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
  }
}
