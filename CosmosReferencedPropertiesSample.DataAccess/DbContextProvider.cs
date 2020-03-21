// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.DataAccess
{
  using System;

  using Microsoft.EntityFrameworkCore;

  public sealed class DbContextProvider : IDbContextProvider
  {
    private readonly Func<DbContext> _dbContextFactory;

    internal DbContextProvider(Func<DbContext> dbContextFactory)
      => _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));

    public DbContext GetNewDbContext() => _dbContextFactory.Invoke();
  }
}
