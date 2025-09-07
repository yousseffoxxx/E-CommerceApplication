namespace ServiceAbstraction
{
    public interface IProductService
    {
        // Get all products
        public Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        // Get product by id
        public Task<ProductDto> GetProductByIdAsync();
        // Get all types
        public Task<IEnumerable<TypeDto>> GetAllTypesAsync();

        // Get all brands
        public Task<IEnumerable<BrandDto>> GetAllBrandsAsync();

    }
}
