namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();

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

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
