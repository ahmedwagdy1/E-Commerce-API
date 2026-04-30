using Domain.Contracts;
using Persistance.Data;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
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
                await _dbContext.SaveChangesAsync();
            }
			catch (Exception ex)
			{
                // Handel exception
			}
        }
    }
}
