// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.DataAccess.Extensions
{
  using System;

  using AutoMapper;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;

  using CosmosReferencedPropertiesSample.DataAccess.Profiles;
  using CosmosReferencedPropertiesSample.DataAccess.Repositories;

  public static class ServicesExtensions
  {
    public static IServiceCollection AddDataAccess(
      this IServiceCollection services,
      string accountEndpoing,
      string accountKey,
      string databaseName)
    {
      if (services == null)
      {
        throw new ArgumentNullException(nameof(services));
      }

      if (string.IsNullOrWhiteSpace(accountEndpoing))
      {
        throw new ArithmeticException($"Argument {nameof(accountEndpoing)} cannot be empty.");
      }

      if (string.IsNullOrWhiteSpace(accountKey))
      {
        throw new ArithmeticException($"Argument {nameof(accountKey)} cannot be empty.");
      }

      if (string.IsNullOrWhiteSpace(databaseName))
      {
        throw new ArithmeticException($"Argument {nameof(databaseName)} cannot be empty.");
      }

      services.AddDbContext<UnitOfWork>(builder =>
        builder.UseCosmos(accountEndpoing, accountKey, databaseName), ServiceLifetime.Transient);

      services.AddScoped<IDbContextProvider>(
        provider => new DbContextProvider(() => provider.GetRequiredService<UnitOfWork>()));

      services.AddScoped<IProductRepository, ProductRepository>();
      services.AddScoped<IOrderRepository, OrderRepository>();

      services.AddAutoMapper(typeof(OrderProducRelationProfile).Assembly);

      return services;
    }
  }
}
