﻿namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.BrandName,
                options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName,
                options => options.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl,
                options => options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();

            CreateMap<ProductType, TypeDto>();
        }
    }
}
