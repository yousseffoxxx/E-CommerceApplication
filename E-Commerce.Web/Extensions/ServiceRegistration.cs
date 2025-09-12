namespace E_Commerce.Web.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static IServiceCollection AddWebApplicationServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;
            });

            return services;
        }
    }
}
