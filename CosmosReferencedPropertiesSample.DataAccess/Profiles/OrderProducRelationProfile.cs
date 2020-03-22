// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosReferencedPropertiesSample.DataAccess.Profiles
{
  using AutoMapper;

  using CosmosReferencedPropertiesSample.DataAccess.Entities;

  internal sealed class OrderProducRelationProfile : Profile
  {
    public OrderProducRelationProfile()
    {
      OrderProducRelationProfile.InitializeProductMappings(this);
      OrderProducRelationProfile.InitializeOrderMappings(this);
    }

    private static void InitializeProductMappings(IProfileExpression expression)
    {
      expression.CreateMap<OrderProductRelationEntity, ProductEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku));

      expression.CreateMap<ProductEntity, OrderProductRelationEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku));
    }

    private static void InitializeOrderMappings(IProfileExpression expression)
    {
      expression.CreateMap<OrderProductRelationEntity, OrderEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId));

      expression.CreateMap<OrderEntity, OrderProductRelationEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id));
    }
  }
}
