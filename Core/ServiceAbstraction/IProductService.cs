namespace ServiceAbstraction
{
    public interface IProductService
    {
        // Get all products
        public Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
        // Get product by id
        public Task<ProductDto> GetProductByIdAsync(int id);
        // Get all types
        public Task<IEnumerable<TypeDto>> GetAllTypesAsync();

        // Get all brands
        public Task<IEnumerable<BrandDto>> GetAllBrandsAsync();

    }
}
