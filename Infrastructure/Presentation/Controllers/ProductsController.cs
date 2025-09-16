namespace Presentation.Controllers
{
    // BaseUrl/api/Products
    public class ProductsController(IServiceManager _serviceManager) : ApiController
    {
        // Get all products
        // GET BaseUrl/api/Products
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);

            return Ok(products);
        }

        // Get product by id 
        // GET BaseUrl/api/Products/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);

            return Ok(product);
        }

        // Get all types
        // GET BaseUrl/api/Products/types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();

            return Ok(types);
        }

        // Get all brands
        // GET BaseUrl/api/Products/brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();

            return Ok(brands);
        }
    }
}
