namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork,
        IMapper _mapper, IBasketRepository _basketRepository,
        UserManager<ApplicationUser> _userManager, IOptions<JwtOptions> _options,
        IConfiguration _configuration, ICacheRepository _cacheRepository) : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        public IProductService ProductService => _lazyProductService.Value;


        private readonly Lazy<IBasketService> _lazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        public IBasketService BasketService => _lazyBasketService.Value;


        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _options, _mapper));
        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
        
        private readonly Lazy<IOrderService> _lazyOrderService = new Lazy<IOrderService>(() => new OrderService(_unitOfWork,_mapper,_basketRepository));
        public IOrderService OrderService => _lazyOrderService.Value;

        private readonly Lazy<IPaymentService> _lazyPaymentService = new Lazy<IPaymentService>(() => new PaymentService(_basketRepository, _unitOfWork, _mapper, _configuration));
        public IPaymentService PaymentService => _lazyPaymentService.Value;

        private readonly Lazy<ICacheService> _lazyCacheService = new Lazy<ICacheService>(() => new CacheService(_cacheRepository));
        public ICacheService CacheService => _lazyCacheService.Value;
    }
}
