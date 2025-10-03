namespace Service
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddAutoMapper(typeof(Service.AssemblyReference).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();
            services.Configure<JwtOptions>(_configuration.GetSection("Jwt"));
            
            return services;
        }
        public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration _configuration)
        {
            var jwtOptions = _configuration.GetSection("Jwt").Get<JwtOptions>();
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    
                    ValidateLifetime = true,                    

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                };
            });

            return services;
        }
    }
}
