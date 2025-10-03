namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddSwaggerServices();
            builder.Services.AddWebApplicationServices(builder.Configuration);
            builder.Services.ConfigureIdentityService();

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddJwtService(builder.Configuration);

            #endregion

            var app = builder.Build();

            #region Data Seeding

            await app.SeedDataBaseAsync();
            #endregion

            #region Configure the HTTP request pipeline.

            app.UseCustomExceptionMiddleWare();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("CORSPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
