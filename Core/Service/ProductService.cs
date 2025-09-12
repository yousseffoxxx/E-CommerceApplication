namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var specifications = new ProductWithBrandAndTypeSpecifications(queryParams);

            var products = await repo.GetAllAsync(specifications);

            var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);

            var productsCount = products.Count();

            var countSpecifications = new ProductCountSpecifications(queryParams);

            var totalCount = await repo.CountAsync(countSpecifications);

            return new PaginatedResult<ProductDto>(queryParams.PageIndex, productsCount, totalCount, productsDto);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(id);

            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);

            if(product is null)
            {
                throw new ProductNotFoundException(id);
            }

            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
        }
    }
}
