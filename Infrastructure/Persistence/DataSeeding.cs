namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext,
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
			try
			{
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync();

                if (!_dbContext.ProductBrands.Any())
                {
                    var productBrandsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");

                    var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);

                    if (productBrands is not null && productBrands.Any())
                        await _dbContext.ProductBrands.AddRangeAsync(productBrands);
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypesData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");

                    var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypesData);

                    if (productTypes is not null && productTypes.Any())
                        await _dbContext.ProductTypes.AddRangeAsync(productTypes);
                }

                if (!_dbContext.Products.Any())
                {
                    var productsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");

                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);

                    if (products is not null && products.Any())
                        await _dbContext.Products.AddRangeAsync(products);
                }
                
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
			{
				//TODO
			}
        }

        public async Task IdentityDataSeedAsync()
        {
            // seed default roles
            if (!_roleManager.Roles.Any())
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            // seed default users
            if (!_userManager.Users.Any())
            {
                var superAdminUser = new ApplicationUser
                {
                    DisplayName = "Youssef",
                    Email = "youssef5fox5@gmail.com",
                    UserName = "youssefSuperAdmin",
                    PhoneNumber = "01556324346"
                };

                var AdminUser = new ApplicationUser
                {
                    DisplayName = "Admin",
                    Email = "admin@gmail.com",
                    UserName = "AdminUser",
                    PhoneNumber = "123456789"
                };

                await _userManager.CreateAsync(superAdminUser,"Passw0rd");
                await _userManager.CreateAsync(AdminUser, "Passw0rd");
                
                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(AdminUser, "Admin");
            }
        }
    }
}
