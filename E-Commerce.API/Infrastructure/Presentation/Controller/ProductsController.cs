using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.Dtos.ProductModule;
using Shared.ErrorModel;

namespace Presentation.Controller
{
  // baseUrl/api/Products
    public class ProductsController(IServiceManger _serviceManger) : ApiController
    {
        #region GetAllProducts
        // EndPoint ==> GetAllProducts
        [HttpGet] // baseUrl/api/Products
        [ProducesResponseType(typeof(ProductResultDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagenationsResult<ProductResultDto>>> GetAllProductsAsync([FromQuery] ProductsSpecificationsParameters parameters)
            => Ok(await _serviceManger.ProductService.GetAllProductsAsync(parameters));
        #endregion

        #region GetAllBrands
        // EndPoint ==> GetAllBrands
        [HttpGet("Brands")] // baseUrl/api/Products/Brands
        [ProducesResponseType(typeof(BrandResultDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
            => Ok(await _serviceManger.ProductService.GetAllBrandsAsync());
        #endregion

        #region GetAllTypes
        // EndPoint ==> GetAllTypes
        [HttpGet("Types")] // baseUrl/api/Products/Types
        [ProducesResponseType(typeof(TypeResultDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
            => Ok(await _serviceManger.ProductService.GetAllTypesAsync());
        #endregion

        #region GetProductById
        // EndPoint ==> GetProductById
        [HttpGet("{id:int}")] // baseUrl/api/Products/5
        [ProducesResponseType(typeof(ProductResultDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductResultDto>> GetProductByIdAsync(int id)
            => Ok(await _serviceManger.ProductService.GetProductByIdAsync(id));
        #endregion
    }
}
