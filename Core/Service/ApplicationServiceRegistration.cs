namespace Service
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Service.AssemblyReference).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();

            return services;
        }
    }
}
