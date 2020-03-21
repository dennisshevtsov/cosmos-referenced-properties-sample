﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.DataAccess
{
  using Microsoft.EntityFrameworkCore;

  public interface IDbContextProvider
  {
    public DbContext GetNewDbContext();
  }
}
