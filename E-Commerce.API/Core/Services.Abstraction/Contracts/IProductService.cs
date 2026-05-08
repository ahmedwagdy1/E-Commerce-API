using Shared;
using Shared.Dtos.ProductModule;
using Shared.Enums;

namespace Services.Abstraction.Contracts
{
    public interface IProductService
    {
        // GetAllProducts
        Task<PagenationsResult<ProductResultDto>> GetAllProductsAsync(ProductsSpecificationsParameters parameters);
        // GetAllBrands
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        // GetAllTypes
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        // GetProductById
        Task<ProductResultDto> GetProductByIdAsync(int id);
    }
}
