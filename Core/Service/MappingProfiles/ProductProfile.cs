namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName,
                options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.TypeName,
                options => options.MapFrom(src => src.ProductType.Name));

            CreateMap<ProductBrand, BrandDto>();

            CreateMap<ProductType, TypeDto>();
        }
    }
}
