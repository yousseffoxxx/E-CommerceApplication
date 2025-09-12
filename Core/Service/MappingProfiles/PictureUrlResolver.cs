using Shared.DTOs.Products;

namespace Service.MappingProfiles
{
    internal class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl)) 
                return string.Empty;

            return $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
