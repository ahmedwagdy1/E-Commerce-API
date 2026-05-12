using Domain.Entities.IdentityModule;
using Domain.Entities.OrderModule;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Persistance
{
    public class DataSeeding(StoreDbContext _dbContext,
        UserManager<User> _userManager,
        RoleManager<IdentityRole> _roleManager
        ) : IDataSeeding
    {
        public async Task SeedDataAsync()
        {
			try
			{
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    //var productTypeData = File.ReadAllText("C:\\Users\\kh\\OneDrive\\Desktop\\Route Course\\back end .net\\Revesion (mariam shindy)\\API\\API Project\\E-Commerce.API\\Infrastructure\\Persistance\\DataSeeding\\types.json")
                    var productTypeData = File.OpenRead("..\\Infrastructure\\Persistance\\DataSeeding\\types.json");
                    // json ==> List<ProductType>
                    var productType = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypeData);
                    if (productType is not null && productType.Any())
                        await _dbContext.ProductTypes.AddRangeAsync(productType);
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var productBrandData = File.OpenRead("..\\Infrastructure\\Persistance\\DataSeeding\\brands.json");
                    // json ==> List<ProductBrands>
                    var productBrand = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandData);
                    if (productBrand is not null && productBrand.Any())
                        await _dbContext.ProductBrands.AddRangeAsync(productBrand);
                }
                if (!_dbContext.Products.Any())
                {
                    var productData = File.OpenRead("..\\Infrastructure\\Persistance\\DataSeeding\\products.json");
                    // json ==> List<ProductType>
                    var product = await JsonSerializer.DeserializeAsync<List<Product>>(productData);
                    if (product is not null && product.Any())
                        await _dbContext.Products.AddRangeAsync(product);
                }
                if (!_dbContext.DeliveryMethods.Any())
                {
                    var deliveryData = File.OpenRead("..\\Infrastructure\\Persistance\\DataSeeding\\delivery.json");
                    // json ==> List<ProductType>
                    var delivery = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(deliveryData);
                    if (delivery is not null && delivery.Any())
                        await _dbContext.DeliveryMethods.AddRangeAsync(delivery);
                }
                await _dbContext.SaveChangesAsync();
            }
			catch (Exception ex)
			{
                // Handel exception
			}
        }

        public async Task SeedIdentityDataAsync()
        {
            try
            {
                // 1. seed roles [Admin, SuperAdmin]
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                // 2. seed users [AdminUser, SuperAdminUser]
                if (!_userManager.Users.Any())
                {
                    var AdminUser = new User()
                    {
                        DisplayName = "AdminUser",
                        UserName = "AdminUser",
                        Email = "AdminUser@gmail.com",
                        PhoneNumber = "1234567890",
                    };
                    var SuperAdminUser = new User()
                    {
                        DisplayName = "SuperAdminUser",
                        UserName = "SuperAdminUser",
                        Email = "SuperAdminUser@gmail.com",
                        PhoneNumber = "1234567891",
                    };
                    await _userManager.CreateAsync(AdminUser, "P@ssw0rd");
                    await _userManager.CreateAsync(SuperAdminUser, "Pa##w0rd");

                    // 3. assign role => user
                    await _userManager.AddToRoleAsync(AdminUser, "Admin");
                    await _userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
                }
            }
            catch (Exception ex)
            {
                // 
            }
        }
    }
}
